using System.Linq;
using TaskSystem.Models;

namespace TaskSystem.Data.Context
{
    public interface ITaskContext
    {
        void SaveChanges();
        void Save<T>(T entity) where T : class;
        IQueryable<UserTask> Tasks { get; }
        IQueryable<UserTaskType> TaskTypes { get; }
    }
}