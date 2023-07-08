using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Entity
{
    public class DriverLicense
    {
        public Guid Id { get; set; }
        public string Serial { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual User Holder { get; set; }
    }
}
