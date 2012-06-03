﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskSystem.Models
{
    public class UserTask
    {
        public UserTask() { }
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1,9)]
        public int TaskPriority { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DueDate { get; set; }

    }
}