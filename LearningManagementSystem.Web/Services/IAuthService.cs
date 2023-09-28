using LearningManagementSystem.Web.ViewModels;

namespace LearningManagementSystem.Web.Services;

public interface IAuthService
{
    Task<(int, string, string)> Registration(RegistrationModel model, string role);
    Task<(int, string)> Login(LoginModel model);
}