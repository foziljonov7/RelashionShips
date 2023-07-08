using LearningWebApi.Entity;

namespace LearningWebApi.Dto
{
    public class GetCourseDto
    {
        public GetCourseDto(Course entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            StartDate = entity.StartDate;
            DateTime = entity.DateTime;
            StudentCount = entity.Users?.Count() ?? 0;
            Students = entity.Users?.Select(x => new GetUserDto(x));
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DateTime { get; }
        public int StudentCount { get; set; }
        public IEnumerable<GetUserDto> Students { get; set; }
    }
}
