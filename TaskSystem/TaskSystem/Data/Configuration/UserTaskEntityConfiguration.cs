﻿using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using TaskSystem.Models;

namespace TaskSystem.Data.Configuration
{
    public class UserTaskEntityConfiguration : EntityTypeConfiguration<UserTask>
    {
        public UserTaskEntityConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.UserId).IsRequired();
            //HasRequired(a => a.TaskType)
            //   .WithMany().HasForeignKey(x => x.TaskType);

            HasRequired(x => x.UserTaskType);


        }
    }
}