namespace TaskSystem.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual UserTaskType TaskType { get; set; }
    }
}