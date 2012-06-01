using System;
using System.Collections.Generic;

namespace TaskSystem.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int TaskPriority { get; set; }
        public DateTime DueDate { get; set; }
        public UserTaskType UserTaskType { get; set; }
    }


}