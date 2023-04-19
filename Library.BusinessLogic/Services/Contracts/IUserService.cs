using Library.Common.ModelsDto;
using Library.Model.Models;

namespace Library.BusinessLogic.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> Get();
        User Get(string Login);
        void EditPassword(User user, string password, string newPassword);
        void Edit(UserDto user, string password);
        void Delete(int id);
    }
}
