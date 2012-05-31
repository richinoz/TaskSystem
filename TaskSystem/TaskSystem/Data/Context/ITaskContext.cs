using System.Linq;
using TaskSystem.Models;

namespace TaskSystem.Data.Context
{
    public interface ITaskContext
    {
        void SaveChanges();
        IQueryable<UserTask> Tasks { get; }
    }
}