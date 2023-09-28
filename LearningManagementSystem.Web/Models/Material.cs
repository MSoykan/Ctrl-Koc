using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LearningManagementSystem.Web.Models;

public class Material
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CourseId { get; set; }
    public string? ImagePath { get; set; }
    public Course Course{ get; set; }
}