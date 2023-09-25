using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Web.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public AppDbContext()
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<Assignment> Assignments { get; set; }


    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentCourse>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId }); // Set a composite primary key

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.User)
            .WithMany(u => u.StudentCourses) // Assuming you have a navigation property in User class for StudentCourses
            .HasForeignKey(sc => sc.StudentId) // Set StudentId as the foreign key
            .OnDelete(DeleteBehavior.NoAction);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=LAPTOP-C413PEVC\\SQLEXPRESS; database=LearningManagementSystem; integrated security=true;TrustServerCertificate = True;");
    }
}
