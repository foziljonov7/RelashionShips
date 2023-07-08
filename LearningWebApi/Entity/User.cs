using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<Car> Cars { get; set; }
        public virtual DriverLicense DriverLicense { get; set; }
    }
}
