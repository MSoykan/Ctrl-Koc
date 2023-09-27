using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Web.Models;

public class Assignment
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string ContentPath { get; set; }
    public int CourseId { get; set; }
    public DateTime DueDate { get; set; }
    public Course Course { get; set; }
    
}