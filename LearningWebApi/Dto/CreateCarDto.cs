using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Dto
{
    public class CreateCarDto
    {
        [Required, MinLength(2), MaxLength(20)]
        public string Brand { get; set; }
        [Required, MinLength(2), MaxLength(20)]
        public string Model { get; set; }
        public string Color { get; set; }

        [Required]
        public DateTime ManufacturedAt { get; set; }
        public Guid OwnerId { get; set; }
    }
}
