using TaskManagement.Data.DTO;

namespace TaskManagement.Manager.Interface
{
    public interface IUsersManager
    {
        Task<LoginModel> LoginUser(UserLoginDto userLogin);
        Task<bool> Register(UsersDto user);
    }
}
