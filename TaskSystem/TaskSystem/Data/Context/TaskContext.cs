using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using TaskSystem.Models;

namespace TaskSystem.Data.Context
{
    /// <summary>
    /// Context for Data Access across the project. 
    /// </summary>
    public class TransactionDomainContext : DbContext, ITaskContext
    {
        public TransactionDomainContext(string nameOrConnectionString)
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

        /// <summary>
        /// Saves Entity
        /// </summary>
        /// <typeparam name="T">type of entity</typeparam>
        /// <param name="entity">entity to be saved</param>
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

        public virtual IDbSet<UserTask> EchoWebServiceRequests { get; set; }

        IQueryable<UserTask> ITaskContext.Tasks
        {
            get { return EchoWebServiceRequests.AsQueryable(); }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new EchoWebServiceRequestConfiguration());
        }
    }
}
