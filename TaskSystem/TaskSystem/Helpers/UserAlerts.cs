using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskSystem.Data.Context;
using TaskSystem.Models;
using System.Linq;

namespace TaskSystem.Helpers
{    
    internal class UserAlert
    {
        public DateTime LastCheck { get; set; }
        public bool Value { get; set; }
    }
    public class UserAlerts
    {
        private readonly ITaskContext _taskContext;

        public UserAlerts()
        {
            _taskContext = StructureMap.ObjectFactory.GetInstance<ITaskContext>();
        }

        private static Dictionary<Guid, UserAlert> _list = new Dictionary<Guid, UserAlert>();

        public static void UpdateUser()
        {
            var user = Membership.GetUser(true);

            var providerUserKey = (Guid)user.ProviderUserKey;
          
            var userAlert = _list[providerUserKey];

            if (userAlert != null)
                userAlert.Value = true;

        }
        [Authorize]
        public IEnumerable<UserTask> GetAlertsForUser()
        {
            var user = Membership.GetUser(true);

            var providerUserKey = (Guid)user.ProviderUserKey;   

            if(!_list.ContainsKey(providerUserKey))
            {
                _list.Add(providerUserKey, new UserAlert() {LastCheck = DateTime.Now, Value = true});
            }
            var userAlert = _list[providerUserKey];            

            if (userAlert.Value)
            {
                var date = DateTime.Now.Date;

                var userTasks = _taskContext.Tasks.Where(x => x.UserId == providerUserKey &&
                                x.DueDate==date);

                var tasks = userTasks.ToList();


                userAlert.Value = false;
                if (tasks.Count > 0)
                    userAlert.Value = true;

                return tasks;
            }
            return new List<UserTask>();
        }
    }
}