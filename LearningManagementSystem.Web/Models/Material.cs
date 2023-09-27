namespace LearningManagementSystem.Web.Models;

public class Material
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContentPath { get; set; }
    public int CourseId { get; set; }
    public Course Course{ get; set; }
}