namespace Tasks.Application.Models
{
    public class TaskCreateDto
    {
        public int? UserId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
