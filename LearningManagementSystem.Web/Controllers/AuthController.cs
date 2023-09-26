using LearningManagementSystem.Web.Models;
using LearningManagementSystem.Web.Services;
using LearningManagementSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LearningManagementSystem.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payload");
            var (status, message) = await _authService.Login(model);
            if (status == 0)
                return BadRequest(message);
            return Ok(message);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payload");
            var (status, message) = await _authService.Registration(model, UserRoles.Admin);
            if (status == 0)
            {
                return BadRequest(message);
            }
            
            string serializedUser = JsonConvert.SerializeObject(model);

            // Store user information in TempData
            TempData["RegisteredUser"] = serializedUser;
            
            return RedirectToAction("Index", "Home");

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // GET
    public IActionResult Login()
    {
        return View("Login");
    }

    public IActionResult Registration()
    {
        return View("Registration");
    }
}