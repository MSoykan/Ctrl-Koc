using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LearningManagementSystem.Web.ViewModels;

public class CourseViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string MaterialTitle { get; set; }
    public string MaterialDescription { get; set; }
    [ValidateNever]
    public IFormFile? Image { get; set; }
    [ValidateNever]
    public string? ImagePath { get; set; }
    public List<AssignmentViewModel> AssignmentViewModels { get; set; }
}