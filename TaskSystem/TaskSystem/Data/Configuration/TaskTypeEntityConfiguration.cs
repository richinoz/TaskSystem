using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using TaskSystem.Models;

namespace TaskSystem.Data.Configuration
{
    public class TaskTypeEntityConfiguration : EntityTypeConfiguration<UserTaskType>
    {
        public TaskTypeEntityConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name);

        }
    }
}