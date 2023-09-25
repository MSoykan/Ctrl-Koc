namespace LearningManagementSystem.Web.Models;

public class Material
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContentPath { get; set; } 
    public List<Course>? Courses { get; set; }
}