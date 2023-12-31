﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Web.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<User>? Users { get; set; }
    public Material? Material{ get; set; }
    public List<Assignment>? Assignments { get; set; }
    public List<StudentCourse>?  StudentCourses { get; set; }
}