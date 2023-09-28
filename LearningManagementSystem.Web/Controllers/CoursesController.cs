using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearningManagementSystem.Web.Models;
using LearningManagementSystem.Web.ViewModels;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Extensions.FileProviders;

namespace LearningManagementSystem.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;

        public CoursesController(AppDbContext context, IFileProvider fileProvider)
        {
            _context = context;
            _fileProvider = fileProvider;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            List<CourseViewModel> courseViewModels = _context.Courses.Include(x => x.Material).Select(x =>
                new CourseViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImagePath = x.Material.ImagePath,
                    MaterialDescription = x.Material.Description,
                    MaterialTitle = x.Material.Title
                }).ToList();
            return View(courseViewModels);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(x => x.Material) // Include related Material data if needed
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            CourseViewModel courseViewModel = new CourseViewModel()
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                ImagePath = course.Material.ImagePath,
                MaterialDescription = course.Material.Description,
                MaterialTitle = course.Material.Title
            };

            return View(courseViewModel);
        }
        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel newCourse)
        {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                try
                {
                    // Create a new Material object and map properties from CourseViewModel
                    var material = new Material
                    {
                        Title = newCourse.MaterialTitle,
                        Description = newCourse.MaterialDescription,
                        ImagePath = null // Initialize ImagePath as null initially
                    };

                    // Create a new Course object and map properties from CourseViewModel
                    var course = new Course
                    {
                        Title = newCourse.Title,
                        Description = newCourse.Description,
                        Material = material // Establish the relationship between Course and Material
                    };

                    if (newCourse.Image is { Length: > 0 })
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");
                        var randomImageName = Guid.NewGuid() + Path.GetExtension(newCourse.Image.FileName);
                        var path = Path.Combine(images.PhysicalPath, randomImageName);
                        using var stream = new FileStream(path, FileMode.Create);
                        newCourse.Image.CopyTo(stream);

                        material.ImagePath = randomImageName; // Update ImagePath in Material
                    }

                    // Add the Course to the context
                    _context.Courses.Add(course);

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception e)
                {
                    result = View();
                }
            }
            else
            {
                result = View();
            }
            return result;
        }


        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var material = await _context.Materials.FindAsync(id);

            if (material == null)
            {
                return NoContent();
            }

            CourseViewModel courseViewModel = new CourseViewModel()
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                MaterialDescription = material.Description,
                MaterialTitle = material.Title,
                ImagePath = material.ImagePath
            };
            
            return View(courseViewModel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing Course and its associated Material
                var existingCourse = await _context.Courses.Include(c => c.Material).FirstOrDefaultAsync(c => c.Id == courseViewModel.Id);

                if (existingCourse == null)
                {
                    return NotFound();
                }

                // Update properties of the existing Material
                existingCourse.Material.Title = courseViewModel.MaterialTitle;
                existingCourse.Material.Description = courseViewModel.MaterialDescription;

                // Update properties of the existing Course
                existingCourse.Title = courseViewModel.Title;
                existingCourse.Description = courseViewModel.Description;

                if (courseViewModel.Image is { Length: > 0 })
                {
                    var root = _fileProvider.GetDirectoryContents("wwwroot");
                    var images = root.First(x => x.Name == "images");
                    var randomImageName = Guid.NewGuid() + Path.GetExtension(courseViewModel.Image.FileName);
                    var path = Path.Combine(images.PhysicalPath, randomImageName);
                    using var stream = new FileStream(path, FileMode.Create);
                    courseViewModel.Image.CopyTo(stream);

                    courseViewModel.ImagePath = randomImageName;
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(courseViewModel);
        }
        

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'AppDbContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
