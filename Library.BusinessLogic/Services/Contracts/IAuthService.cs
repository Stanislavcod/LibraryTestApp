using Library.Common.ModelsDto;

namespace Library.BusinessLogic.Services.Contracts
{
    public interface IAuthService
    {
        void Register(UserDto userDto, string confirmPassword);
        void Login(UserDto userDto);
        void RegisterAdmin(UserDto userDto);
    }
}
