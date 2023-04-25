using Library.BusinessLogic.Services.Contracts;
using Library.Common.Helpers.Cryptography;
using Library.Common.ModelsDto;
using Library.Model.DatabaseContext;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.BusinessLogic.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly ApplicationDatabaseContext _context;
        private readonly IUserService _userService;
        public AuthService(ILogger<AuthService> logger, ApplicationDatabaseContext context, IUserService userService)
        {
            _logger = logger;
            _context = context;
            _userService = userService;
        }
        public void Register(UserDto newUser, string confirmPassword)
        {
            try
            {
                var existingUserByName = _context.Users.Any(u => u.Login == newUser.Login);
                if (existingUserByName)
                    throw new Exception("Пользователь с таким именем уже существует");

                var existingUserByEmail = _context.Users.Any(u => u.Email == newUser.Email);
                if (existingUserByEmail)
                    throw new Exception("Пользователь с такой почтой уже существует");

                if (newUser.Password != confirmPassword)
                    throw new Exception("Пароли не совпадают!");

                PasswordHasher.CreatePasswordHash(newUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var user = new User
                {
                    Login = newUser.Login,
                    Email = newUser.Email,
                    RoleId = 1,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }
        public void RegisterAdmin(UserDto userDto)
        {
            var existingUser = _context.Users.Any(u=> u.Login == userDto.Login);
            if (existingUser)
                throw new Exception("Пользователь с такими именем уже сужествует");

            PasswordHasher.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var admin = new User
            {
                Login = userDto.Login,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 2
            };

            _context.Users.Add(admin);
            _context.SaveChanges();
        }
        public void Login(UserDto userDto)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == userDto.Login);
            if (user == null)
                throw new Exception("Пользователь не найден");
            if (PasswordHasher.VerifyPasswordHash(user.PasswordSalt, user.PasswordHash, userDto.Password))
                throw new Exception("Неверный пароль!");
        }
    }
}
