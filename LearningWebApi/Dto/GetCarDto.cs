namespace LearningWebApi.Dto
{
    public class GetCarDto
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public DateTime ManufacturedAt { get; set; }
        public Guid OwnerId { get; set; }
        public GetUserDto Owner { get; set; }
    }
}
