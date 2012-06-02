using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using TaskSystem.Models;

namespace TaskSystem.Helpers
{    
    public class UserAlerts
    {
        private static UserAlerts _userAlerts;

        private MembershipUser _membershipUser;
        private IEnumerable<UserTask> _userTasks;
        private DateTime _lastCheck;

        public static IEnumerable<UserTask> GetAlertsForUser()
        {
            if(_userAlerts==null)
            {
               
               // HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "test");

                var membershipUser = Membership.GetUser(true);
                var userAlerts = new UserAlerts(){ _lastCheck = DateTime.Now};
                string encryptedTicket = FormsAuthentication.Encrypt(userAlerts);

                HttpCookie cookie = new HttpCookie(membershipUser.UserName, "test");

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}