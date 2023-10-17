using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using System.Linq;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool HasErrorsResponse(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                foreach (var item in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return true;
            }

            return false;
        }
    }
}
