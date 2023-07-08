using LearningWebApi.Entity;

namespace LearningWebApi.Dto
{
    public class GetUserDto
    {
        public GetUserDto(User entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Birthday = entity.Birthday;
            Email = entity.Email;
            Username = entity.Username;
            DriverLicense = entity.DriverLicense is null ? null : new GetDriverLicenseDto(entity.DriverLicense);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public GetDriverLicenseDto DriverLicense { get; set; }
    }
}
