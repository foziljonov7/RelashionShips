using LearningWebApi.Entity;
using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Dto
{
    public class CreateCourseDto
    {
        [Required, MinLength(2), MaxLength(20)]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DateTime { get; set; }
    }
}

   

