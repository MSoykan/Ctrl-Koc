using LearningManagementSystem.Web.ViewModels;

namespace LearningManagementSystem.Web.Services;

public interface IAuthService
{
    Task<(int, string, string)> Registration(RegistrationModel model);
    Task<(int, string)> Login(LoginModel model);
}