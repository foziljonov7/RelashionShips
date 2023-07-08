using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Entity
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DateTime { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
