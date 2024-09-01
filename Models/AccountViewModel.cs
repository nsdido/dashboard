using System.ComponentModel.DataAnnotations;

namespace Climate_Watch.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter the {0} ")]
        [MaxLength(255)]
        [Display(Name ="TelegramId")]
        public string TelegramId { get; set; }
        [Required(ErrorMessage = "Please Enter the {0} ")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter the {0} ")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name ="Re Password")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter the {0} ")]
        [MaxLength(255)]
        
        [Display(Name = "TelegramId")]
        public string TelegramId { get; set; }
        [Required(ErrorMessage = "Please Enter the {0} ")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
