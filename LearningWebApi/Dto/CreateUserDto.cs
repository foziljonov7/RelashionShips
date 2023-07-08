namespace LearningWebApi.Dto
{
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}
