using LearningWebApi.Data;
using LearningWebApi.Dto;
using LearningWebApi.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace LearningWebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        public UsersController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            if (await dbContext.Users.AnyAsync(u => u.Username.ToLower() == user.Username.ToLower()))
                return Conflict("User with this username exists");
            if (await dbContext.Users.AnyAsync(u => u.Email.ToLower() == user.Email.ToLower()))
                return Conflict("User with this email exists");

            var created = dbContext.Users.Add(new Entity.User
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
                Birthday = user.Birthday,
                Username = user.Username,
            });
            await dbContext.SaveChangesAsync();

            return Ok(created.Entity.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users
                .Where(u => u.Id == id)
                .Include(u => u.DriverLicense)
                .FirstOrDefaultAsync();
            if (user is null)
                return NotFound();
            return Ok(new GetUserDto(user));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] string search)
        {
            var usersQuery = dbContext.Users.AsQueryable();

            if(false == string.IsNullOrWhiteSpace(search))
                usersQuery = usersQuery.Where(u => 
                u.Name.ToLower().Contains(search.ToLower()) ||
                u.Username.ToLower().Contains(search.ToLower()));

            var users = await usersQuery
                .Select(u => new GetUserDto(u))
                .ToListAsync();

            return Ok(users);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUserDto updateUser)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if(user is null) 
                return NotFound();
            if (await dbContext.Users.AnyAsync(u => u.Username.ToLower() == updateUser.Username.ToLower()))
                return Conflict("User with this username exists");
            if (await dbContext.Users.AnyAsync(u => u.Email.ToLower() == updateUser.Email.ToLower()))
                return Conflict("User with this email exists");
            user.Name = updateUser.Name;
            user.Username = updateUser.Username;
            user.Email = updateUser.Email;
            user.Birthday = updateUser.Birthday;

            await dbContext.SaveChangesAsync();

            return Ok(user.Id);
        }
        [HttpDelete("{id")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if(user is null) 
                return NotFound();
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("{userId}/driver-license")]
        public async Task<IActionResult> CreateDriverLicense(Guid userId, CreateDriverLicenseDto driverLicense)
        {
            var user = await dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.DriverLicense)
                .FirstOrDefaultAsync();

            if (user is null)
                return BadRequest($"User with id {userId} does not exists");
            if (user.DriverLicense is not null)
                return BadRequest($"User already has driver license");

            user.DriverLicense = new DriverLicense
            {
                Serial = driverLicense.Serial,
                IssuedDate = driverLicense.IssuedDate,
                ExpirationDate = driverLicense.ExpirationDate
            };

            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/driver-license")]
        public async Task<IActionResult> GetUserDriverLicense([FromRoute] Guid id)
        {
            var user = await dbContext.Users
                .Where(u => u.Id == id)
                .Include(u => u.DriverLicense)
                .FirstOrDefaultAsync();

            if (user is null || user.DriverLicense is null)
                return NotFound();

            return Ok(new GetDriverLicenseDto(user.DriverLicense));
        }
    }
}
