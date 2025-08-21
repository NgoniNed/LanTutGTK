using System;
namespace LanTutor.Services
{
    using LanTutor.DataModels;
    using LanTutor.Database;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly LanTutorContext _context;

        public UserService(LanTutorContext context)
        {
            _context = context;
        }

        public bool RegisterUser(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username)) return false;

            var hashed = HashPassword(password);
            _context.Users.Add(new User
            {
                Username = username,
                PasswordHash = hashed,
                Role = "Learner"
            });
            _context.SaveChanges();
            return true;
        }

        public User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool ValidateUser(string username, string password)
        {
            var user = GetUser(username);
            return user != null && VerifyPassword(password, user.PasswordHash);
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }

}
