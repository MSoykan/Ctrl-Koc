using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Web.Models;

public class Assignment
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string ContentPath { get; set; }
    [ForeignKey("UserId")]
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime DueDate { get; set; }
    public List<User>? Users { get; set; }
    public List<Course>? Courses { get; set; }
    
}