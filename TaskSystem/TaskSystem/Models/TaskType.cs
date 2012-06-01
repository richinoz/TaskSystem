using System;
using System.Collections.Generic;

namespace TaskSystem.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public UserTaskType UserTaskType { get; set; }
    }


}