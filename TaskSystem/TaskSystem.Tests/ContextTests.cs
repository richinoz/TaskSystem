using System;
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

                var userTask = new UserTask() { Description = "test", DueDate = DateTime.Now, TaskPriority = 1, UserId = Guid.NewGuid()};
                context.Save(userTask);
                context.SaveChanges();

                var ret = context.Tasks.First();

                Assert.AreEqual(userTask, ret);
            }
        }

    }

    // ReSharper restore InconsistentNaming
}