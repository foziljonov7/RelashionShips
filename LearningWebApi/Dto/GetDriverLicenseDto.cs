using LearningWebApi.Entity;

namespace LearningWebApi.Dto
{
    public class GetDriverLicenseDto
    {
        public GetDriverLicenseDto(DriverLicense entity)
        {
            Id = entity.Id;
            Serial = entity.Serial;
            IssuedDate = entity.IssuedDate;
            ExpirationDate = entity.ExpirationDate;
        }

        public Guid Id { get; set; }
        public string Serial { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired => ExpirationDate < DateTime.UtcNow;
    }
}
