namespace LearningWebApi.Dto
{
    public class CreateDriverLicenseDto
    {
        public string Serial { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
