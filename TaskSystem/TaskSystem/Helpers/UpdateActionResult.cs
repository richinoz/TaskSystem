using System;
using System.Web.Mvc;

namespace TaskSystem.Helpers
{
    /// <summary>
    /// This action result
    /// </summary>
    public class UpdateActionResult : ActionResult
    {
        private readonly ActionResult _actionResult;
        private Action _action { get; set; }

        public UpdateActionResult(ActionResult actionResult, Action action)
        {
            _actionResult = actionResult;
            _action = action;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (_action != null)
                _action.Invoke();

            UserAlerts.UpdateUser();

            _actionResult.ExecuteResult(context);

        }
    }
}