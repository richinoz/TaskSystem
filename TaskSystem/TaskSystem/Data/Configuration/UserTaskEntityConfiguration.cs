using System.ComponentModel.DataAnnotations;
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

            //HasRequired(a => a.TaskType)
            //   .WithMany().HasForeignKey(x => x.TaskType);

             HasRequired(x => x.TaskType);//..Map(x => x.MapKey("TransactionID"));

            //Property(x => x.TaskType);

            //Property(x => x.StatusId).HasColumnName("Status").IsRequired();
            //Property(x => x.EchoServiceCallId).HasColumnName("EchoServiceCall").IsRequired();
            //Property(x => x.DateModified).IsOptional();
            //Property(x => x.DateAdded).IsRequired();
            //Property(x => x.FailureMessage).IsOptional().IsMaxLength();
            //Property(x => x.Attempts).IsOptional();
            //Property(x => x.TransactionId).IsRequired();


        }
    }
}