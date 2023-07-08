namespace LearningWebApi.Dto
{
    public class AddCourseStudentsDto
    {
        public IEnumerable<Guid> StudentIds { get; set; }
    }
}
