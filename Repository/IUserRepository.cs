using Climate_Watch.Data;
using Climate_Watch.Models;
using Microsoft.EntityFrameworkCore;

namespace Climate_Watch.Repository
{
    public interface IUserRepository
    {
        bool IsEmailValids(string email);
        void AddUser(Users user);
        void EditUser(Users user);
        Users GetUser(string email, string password);
        Users GetUser(string telegramId);
    }
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsEmailValids(string email)
        {
            return _context.Users.Any(e => e.TelegramId == email);
        }
        public void AddUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public Users GetUser(string email, string password)
        {
            return _context.Users
                .SingleOrDefault(w => w.TelegramId == email && w.Password == password);
        }
        public Users GetUser(string telegramId)
        {
            return _context.Users
                .FirstOrDefault(w => w.TelegramId == telegramId);
        }
        public void EditUser(Users user)
        {
            _context.Attach(user).State = EntityState.Modified;

            _context.SaveChangesAsync();

        }
    }
}
