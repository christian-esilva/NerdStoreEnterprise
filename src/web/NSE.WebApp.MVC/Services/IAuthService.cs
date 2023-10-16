using NSE.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public interface IAuthService
    {
        Task<UserLoginResponse> Login(UserLoginViewModel userLogin);
        Task<UserLoginResponse> Register(UserRegisterViewModel userRegister);
    }
}
