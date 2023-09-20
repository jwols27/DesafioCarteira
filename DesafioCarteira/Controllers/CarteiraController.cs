using DesafioCarteira.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DesafioCarteira.Controllers
{
    public class CarteiraController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEntrada(string entrada, string pessoa)
        {
            Entrada convertido = JsonConvert.DeserializeObject<Entrada>(entrada);

            return Json(convertido);
        }

        //return RedirectToAction(nameof(Error), new { message = "ID not provided" });

        //public IActionResult Error(string message)
        //{
        //    var viewModel = new ErrorViewModel
        //    {
        //        Message = message,
        //        RequestId = Activity.Current?.Id
        //    };
        //    return View(viewModel);
        //}
    }
}
