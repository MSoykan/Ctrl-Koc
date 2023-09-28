using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LearningManagementSystem.Web.Models;
using LearningManagementSystem.Web.ViewModels;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Newtonsoft.Json;

namespace LearningManagementSystem.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string mode = "")
    {
        
        // Check if a mode is specified and store it in session
        if (!string.IsNullOrEmpty(mode))
        {
            HttpContext.Session.SetString("Mode", mode);
        }
        else
        {
            // If mode is not specified in the URL, check if it's stored in session
            mode = HttpContext.Session.GetString("Mode");
        }

        if (TempData.ContainsKey("RegisteredUser") && mode == "register")
        {
            // TempData contains data from registration, so retrieve and use it
            var registeredUserData = TempData["RegisteredUser"].ToString();
            if (!string.IsNullOrEmpty(registeredUserData))
            {
                var registeredUserModel = JsonConvert.DeserializeObject<LoggedInUserViewModel>(registeredUserData);
                HttpContext.Session.SetString("RegisteredUser", registeredUserData);
                return View(registeredUserModel);
            }
            var registeredUserDataFromSession = HttpContext.Session.GetString("RegisteredUser");
            var registeredUserModelFromSession =
                JsonConvert.DeserializeObject<LoggedInUserViewModel>(registeredUserDataFromSession);
            return View(registeredUserModelFromSession);
        }

        if (mode == "login")
        {
            var loggedInUserData = TempData["LoggedInUser"]?.ToString(); // Use null conditional operator to handle null value
            if (!string.IsNullOrEmpty(loggedInUserData))
            {
                var loggedInUserModel = JsonConvert.DeserializeObject<LoggedInUserViewModel>(loggedInUserData);
                HttpContext.Session.SetString("LoggedInUser", loggedInUserData);

                return View(loggedInUserModel);
            }

            var loggedInUserDataFromSession = HttpContext.Session.GetString("LoggedInUser");
            var loggedInUserModelFromSession =
                JsonConvert.DeserializeObject<LoggedInUserViewModel>(loggedInUserDataFromSession);
            return View(loggedInUserModelFromSession);
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}