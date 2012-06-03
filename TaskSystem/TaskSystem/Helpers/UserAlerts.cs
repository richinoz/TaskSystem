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
    
    /// <summary>
    /// UserAlerts class reduces hots on database reads by checking if previous reads
    /// where successful or not
    /// </summary>
    public class UserAlerts
    {
        private class UserAlert
        {
            public bool Value { get; set; }
            public DateTime LastChecked{ get; set; }
        }

        private static Dictionary<Guid, UserAlert> _list = new Dictionary<Guid, UserAlert>();

        public static void UpdateUser()
        {
            var user = Membership.GetUser(true);

            if (user != null)
            {
                var providerUserKey = (Guid) user.ProviderUserKey;

                var userAlert = _list[providerUserKey];

                if (userAlert != null)
                    userAlert.Value = true;
            }

        }
        [Authorize]
        public static IEnumerable<UserTask> GetAlertsForUser()
        {
            var user = Membership.GetUser(true);

            var providerUserKey = (Guid)user.ProviderUserKey;   

            if(!_list.ContainsKey(providerUserKey))
            {
                _list.Add(providerUserKey, new UserAlert() { Value = true, LastChecked = DateTime.Now.Date});
            }
            var userAlert = _list[providerUserKey];            

            var date = DateTime.Now.Date;

            if (userAlert.Value || userAlert.LastChecked!=date)
            {
                ITaskContext taskContext = StructureMap.ObjectFactory.GetInstance<ITaskContext>();
                var userTasks = taskContext.Tasks.Where(x => x.UserId == providerUserKey &&
                                x.DueDate==date);

                var tasks = userTasks.ToList();

                userAlert.Value = false;
                userAlert.LastChecked = DateTime.Now.Date;

                if (tasks.Count > 0)
                    userAlert.Value = true;

                return tasks;
            }
            return new List<UserTask>();
        }

        
    }
}