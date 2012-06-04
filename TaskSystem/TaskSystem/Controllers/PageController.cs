using System.Web.Mvc;
using TaskSystem.Models;

namespace TaskSystem.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Error(string errorCode)
        {
            var viewModel = new PageViewModel();

            int code = 0;
            int.TryParse(errorCode, out code);

            switch (code)
            {
                case 403:
                    viewModel.HtmlTitleTag = "403 Forbidden";
                    break;
                case 404:
                    viewModel.HtmlTitleTag = "404 Page Not Found";
                    break;
                case 500:
                    viewModel.HtmlTitleTag = "500 Internal Server Error";
                    break;
                default:
                    viewModel.HtmlTitleTag = "Generic Error";
                    break;
            }

            return View(viewModel);
        }

    }
}