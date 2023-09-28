using LearningManagementSystem.Web.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Web.Views.Shared.ViewComponents;

public class RoleBasedNavBarViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RoleBasedNavBarViewComponent(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var isAdmin = JwtUtils.IsUserAdminBasedOnToken(_httpContextAccessor.HttpContext);
        // Pass the isAdmin value to the view component
        return View(isAdmin);
    }
}