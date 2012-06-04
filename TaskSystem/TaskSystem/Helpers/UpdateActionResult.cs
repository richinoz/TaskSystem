using System;
using System.Web.Mvc;

namespace TaskSystem.Helpers
{
    /// <summary>
    /// This action result
    /// </summary>
    public class UpdateActionResult : ActionResult
    {
        private readonly ActionResult _actionSuccess;
        private readonly ActionResult _actionFailure;
        private Action _action { get; set; }

        public UpdateActionResult(ActionResult actionSuccess, ActionResult actionFailure, Action action)
        {
            _actionSuccess = actionSuccess;
            _actionFailure = actionFailure;
            _action = action;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context.Controller.ViewData.ModelState.IsValid)
            {
                if (_action != null)
                    _action.Invoke();

                UserAlerts.UpdateUser();

                _actionSuccess.ExecuteResult(context);
            }

            _actionFailure.ExecuteResult(context);
        }
    }
}