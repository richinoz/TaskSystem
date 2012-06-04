using System.Web.Mvc;
using System.Web.Security;

namespace TaskSystem.Helpers
{
    public class AuthorizeTaskSystemAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (base.AuthorizeCore(httpContext))
            {
                var user = Membership.GetUser(true);
                if (user != null)
                    return true;

                FormsAuthentication.SignOut();

            }

            return false;
        }
    }
}