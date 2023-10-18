using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSE.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if(id == 500)
            {
                modelError.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte";
                modelError.Title = "Ocorreu um erro";
                modelError.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelError.Message = "A página que está procurando não existe! <br /> Em caso de dúvidas contate nosso suporte";
                modelError.Title = "Página não encontrada";
                modelError.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelError.Message = "Você não tem permissão para fazer isto";
                modelError.Title = "Acesso negado";
                modelError.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelError);
        }
    }
}
