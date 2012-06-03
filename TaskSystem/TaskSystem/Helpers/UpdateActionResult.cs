using System.Web.Mvc;

namespace TaskSystem.Helpers
{
    /// <summary>
    /// This action result
    /// </summary>
    public class UpdateActionResult : ActionResult
    {
        private readonly ActionResult _actionResult;

        public UpdateActionResult(ActionResult actionResult)
        {
            _actionResult = actionResult;
        }

        public override void ExecuteResult(ControllerContext context)
        {            
            UserAlerts.UpdateUser();
            
            _actionResult.ExecuteResult(context);
        }
    }
}