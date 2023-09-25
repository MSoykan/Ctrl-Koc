namespace LearningManagementSystem.Web.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public char Role { get; set; }
    public List<Course>? Courses { get; set; }
    public List<Assignment>? Assignments { get; set; }
    public List<StudentCourse>? StudentCourses { get; set; }
}