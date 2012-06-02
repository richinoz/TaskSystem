using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskSystem.Data.Context;
using TaskSystem.Models;


namespace TaskSystem.Tests
{
    // ReSharper disable InconsistentNaming

    [TestClass]
    public class ContextTests
    {
        [TestInitialize]
        public void Setup()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TaskContext>());
        }
        [TestMethod]
        public void Index()
        {
            using (var context = new TaskContext("TaskSystemWebsite"))
            {
                var taskType = new UserTaskType() { Name = "TaskType1" };
                context.Save(taskType);
                context.SaveChanges();

                var userTask = new UserTask() { Description = "test", UserTaskType = taskType };
                context.Save(userTask);
                context.SaveChanges();

                var ret = context.Tasks.First();

                Assert.AreEqual(taskType, ret.UserTaskType);
            }
        }

    }

    // ReSharper restore InconsistentNaming
}