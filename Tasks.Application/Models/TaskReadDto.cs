namespace Tasks.Application.Models
{
    public class TaskReadDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }

        public string Description { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

    }
}
