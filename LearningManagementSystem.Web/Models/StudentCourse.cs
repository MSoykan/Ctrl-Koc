using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Web.Models;

public class StudentCourse
{
    public string StudentId { get; set; }
    public int CourseId { get; set; }
    public bool CompletionStatus { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public User User { get; set; }
    public Course Course { get; set; }
}