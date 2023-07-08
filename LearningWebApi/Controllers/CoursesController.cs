using LearningWebApi.Data;
using LearningWebApi.Dto;
using LearningWebApi.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace LearningWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public CoursesController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromRoute] CreateCourseDto course)
        {
            var savedEntity = dbContext.Courses.Add(new Course
            {
                Name = course.Name,
                StartDate = course.StartDate,
                DateTime = course.DateTime
            });

            await dbContext.SaveChangesAsync();

            return CreatedAtAction(
                actionName: nameof(CreateCourse),
                routeValues: new { Id = savedEntity.Entity.Id },
                value: new GetCourseDto(savedEntity.Entity));
        }
        [HttpGet("{id")]
        public async Task<IActionResult> GetCourse([FromRoute] Guid id)
        {
            var course = await dbContext.Courses
                .Where(e => e.Id == id)
                .Include(e => e.Users)
                .FirstOrDefaultAsync();
            if (course is null)
                return NotFound();

            return Ok(new GetCourseDto(course));
        }
        public async Task<IActionResult> AddCourseStudents([FromRoute] Guid id, [FromBody] AddCourseStudentsDto dto)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(e => e.Id == id);

            if (course is null) 
                return NotFound();

            if (dto.StudentIds.Any() is false)
                return BadRequest("Send at lest one Student ID.");

            foreach(var studentId in dto.StudentIds)
            {
                if(false == await dbContext.Courses.AnyAsync(c => c.Id == studentId))
                    return BadRequest($"Student with ID {studentId} does not exist.");
            }

            var students = await dbContext.Users
                .Where(u => dto.StudentIds.Contains(u.Id))
                .ToListAsync();

            course.Users.AddRange(students);
            await dbContext.SaveChangesAsync();

            return Ok(new GetCourseDto(course));
        }
    }
}