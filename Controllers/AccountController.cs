using System.Security.Claims;
using Climate_Watch.Models;
using Climate_Watch.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Climate_Watch.Controllers {
    public class AccountController : Controller {
        private IUserRepository _userRepository;

        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<Users> _passwordHasher;


        public AccountController(IUserRepository userRepository, IConfiguration configuration,
            IPasswordHasher<Users> passwordHasher)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registermodel)
        {
            if (!ModelState.IsValid)
            {
                return View(registermodel);
            }

            if (_userRepository.IsEmailValids(registermodel.TelegramId.ToLower()))
            {
                ModelState.AddModelError("TelegramId", "This telegram id has been registered already");
                return View(registermodel);
            }

            Users user = new Users()
            {
                TelegramId = registermodel.TelegramId.ToLower(),
                RegisterDate = DateTime.Now.Date,
                IsAdmin = true
            };

            user.Password = _passwordHasher.HashPassword(user, registermodel.Password);
            _userRepository.AddUser(user);
            return View("SuccessRegister", registermodel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = _userRepository.GetUser(loginViewModel.TelegramId);
            if (user == null)
            {
                ModelState.AddModelError("TelegramId", "Wrong data");
                return View(loginViewModel);
            }

            var passwordVerificationResult = _passwordHasher
                .VerifyHashedPassword(user, user.Password,
                    loginViewModel.Password);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("Password", "Wrong password");
                return View(loginViewModel);
            }

            // Create JWT token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TelegramId),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim("IsAdmin", user.IsAdmin.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var princial = new ClaimsPrincipal(identity);
            var propertise = new AuthenticationProperties
            {
                IsPersistent = loginViewModel.RememberMe
            };
            
            HttpContext.SignInAsync(princial, propertise);
            
            // Store user info in session
            HttpContext.Session.SetInt32("IsAuthenticated", 1);
            HttpContext.Session.SetString("IsSuperAdmin", user.IsSuperAdmin.ToString());

            return Redirect("/");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        public IActionResult ShowUserInfo()
        {
            var user = _userRepository.GetUser(User.Identity.Name);
            return View(user);
        }

        public IActionResult EditUser()
        {
            var user = _userRepository.GetUser(User.Identity.Name);
            return View(user);
        }
    }
}