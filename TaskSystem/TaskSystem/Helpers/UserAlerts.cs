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
    /// Only users with tasks for today will hit the database (after inital read)
    /// </summary>
    [Authorize]
    public class UserAlerts
    {
        private class UserAlert
        {
            public bool Value { get; set; }
            public DateTime LastChecked { get; set; }
        }

        private static Dictionary<Guid, UserAlert> _userList = new Dictionary<Guid, UserAlert>();
        private static DateTime _listCreateDate = DateTime.Now.Date;
        /// <summary>
        /// Force GetAlertsForUser() to search datasource
        /// </summary>
        public static void UpdateUser()
        {
            var user = Membership.GetUser(true);

            if (user != null)
            {
                var providerUserKey = (Guid)user.ProviderUserKey;

                var userAlert = _userList[providerUserKey];

                if (userAlert != null)
                    userAlert.Value = true;
            }

        }

        /// <summary>
        /// gets or creates current user task status
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<UserTask> GetAlertsForUser()
        {
            var date = DateTime.Now.Date;

            if (_listCreateDate.AddDays(1) < date)
            {
                //clear list if over a day old - to prevent list continually growing
                _userList.Clear();
                _listCreateDate = date;
            }

            var user = Membership.GetUser(true);

            var providerUserKey = (Guid)user.ProviderUserKey;

            if (!_userList.ContainsKey(providerUserKey))
            {
                _userList.Add(providerUserKey, new UserAlert() { Value = true, LastChecked = DateTime.Now.Date });
            }
            var userAlert = _userList[providerUserKey];



            if (userAlert.Value || userAlert.LastChecked != date)
            {
                ITaskContext taskContext = StructureMap.ObjectFactory.GetInstance<ITaskContext>();
                var userTasks = taskContext.Tasks.Where(x => x.UserId == providerUserKey &&
                                x.DueDate == date);

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