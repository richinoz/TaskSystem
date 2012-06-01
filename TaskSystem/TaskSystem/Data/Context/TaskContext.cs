using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using TaskSystem.Data.Configuration;
using TaskSystem.Models;

namespace TaskSystem.Data.Context
{
    /// <summary>
    /// Context for Data Access across the project. 
    /// </summary>
    public class TaskContext : DbContext, ITaskContext
    {
        public TaskContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Flushes changes to underlying Database
        /// </summary>
        void ITaskContext.SaveChanges()
        {
            try
            {
                SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var exceptionMessage = new StringBuilder();
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    exceptionMessage.AppendLine(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                }

                throw new ApplicationException(exceptionMessage.ToString(), dbEx);
            }
        }

        public void Save<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }

        /// <summary>
        /// Deletes Entity
        /// </summary>
        /// <typeparam name="T">type of entity</typeparam>
        /// <param name="id">entity to be deleted</param>
        public void Delete<T>(long id) where T : class
        {
            var saved = Set<T>().Find(id);
            Set<T>().Remove(saved);
        }

        public virtual IDbSet<UserTask> Tasks { get; set; }

        IQueryable<UserTask> ITaskContext.Tasks
        {
            get { return Tasks.AsQueryable(); }
        }

        public virtual IDbSet<UserTaskType> TaskTypes { get; set; }

        IQueryable<UserTaskType> ITaskContext.TaskTypes
        {
            get { return TaskTypes.AsQueryable(); }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserTaskEntityConfiguration());
            modelBuilder.Configurations.Add(new TaskTypeEntityConfiguration());
        }

        public class Initializer : IDatabaseInitializer<TaskContext>
        {
            public void InitializeDatabase(TaskContext context)
            {
                if (!context.Database.Exists())
                {
                    context.Database.Create();

                    var userTaskType = new UserTaskType() { Name = "Example Task Type" };
                    context.Save(userTaskType);
                    context.SaveChanges();
                }
            }
        }
    }
}
