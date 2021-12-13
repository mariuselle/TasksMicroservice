using Tasks.Domain.Enums;

namespace Tasks.Domain.Entities
{
    public class Task : EntityBase
    {
        public int? UserId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
    }
}
