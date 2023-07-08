using LearningWebApi.Data;
using LearningWebApi.Dto;
using LearningWebApi.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public CarsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarDto carDto)
        {
            var owner = await dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == carDto.OwnerId);

            if (owner is null)
                return BadRequest("Owner does not exist.");

            var savedCar = dbContext.Cars.Add(new Car
            {
                Id = Guid.NewGuid(),
                Brand = carDto.Brand,
                Model = carDto.Model,
                ManufacturedAt = carDto.ManufacturedAt,
                Color = carDto.Color,
                Owner = owner,
            });

            await dbContext.SaveChangesAsync();

            return Ok(new GetCarDto
            {
                Id = savedCar.Entity.Id,
                Brand = savedCar.Entity.Brand,
                Model = savedCar.Entity.Model,
                Color = savedCar.Entity.Color,
                ManufacturedAt = savedCar.Entity.ManufacturedAt,
                OwnerId = savedCar.Entity.OwnerId,
                Owner = new GetUserDto(savedCar.Entity.Owner)
            });
        }
    }
}
