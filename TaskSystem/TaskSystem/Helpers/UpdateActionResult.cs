using System;
using System.Web.Mvc;

namespace TaskSystem.Helpers
{
    /// <summary>
    /// This action result
    /// </summary>
    public class FormActionResult : ActionResult
    {
        private readonly ActionResult _actionSuccess;
        private readonly ActionResult _actionFailure;
        private Action _action { get; set; }

        public FormActionResult(ActionResult actionSuccess, ActionResult actionFailure, Action action)
        {
            _actionSuccess = actionSuccess;
            _actionFailure = actionFailure;
            _action = action;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            try
            {
                if (_action != null)
                    _action.Invoke();

                UserAlerts.UpdateUser();

                _actionSuccess.ExecuteResult(context);
            }
            catch (Exception ex)
            {
                context.Controller.ViewData.ModelState.AddModelError(@"", ex.ToString());
                _actionFailure.ExecuteResult(context);
            }
        }
    }


}