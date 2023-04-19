using Library.BusinessLogic.Services.Contracts;
using Library.Common.Helpers.Cryptography;
using Library.Common.ModelsDto;
using Library.Model.DatabaseContext;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<User> _logger;
        private readonly ApplicationDatabaseContext _context;
        public UserService(ApplicationDatabaseContext context, ILogger<User> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IEnumerable<User> Get()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
                return new List<User>();
            }
        }
        public User Get(string login)
        {
            try
            {
                return _context.Users.FirstOrDefault(x => x.Login == login);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
                return new User();
            }
        }
        public void Edit(UserDto request, string password)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == request.Id);
                if (user == null)
                    throw new Exception("Пользователь не найден");

                if (!string.Equals(user.Login, request.Login, StringComparison.OrdinalIgnoreCase))
                {
                    var existingUser = _context.Users.FirstOrDefaultAsync(u => u.Login == request.Login);
                    if (existingUser != null)
                        throw new Exception("Пользователь с таким именем уже существует");
                    user.Login = request.Login;
                }

                if (!string.IsNullOrEmpty(password))
                {
                    PasswordHasher.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }

                if (user.RoleId != request.RoleId)
                    user.RoleId = request.RoleId;

                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }
        public void EditPassword(User user, string password, string newPassword)
        {
            try
            {
                if (!PasswordHasher.VerifyPasswordHash(user.PasswordSalt, user.PasswordHash, password))
                    throw new Exception("Неверный пароль");
                PasswordHasher.CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] newPasswordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = newPasswordSalt;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }
        public void Delete(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);

                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }
    }
}
