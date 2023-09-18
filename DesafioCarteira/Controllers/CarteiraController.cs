using DesafioCarteira.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DesafioCarteira.Controllers
{
    public class CarteiraController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movimento movimento)
        {
            if (!ModelState.IsValid)
            {
                return View(movimento);
            }

            return View();
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Movimento movimento)
        {
            if (!ModelState.IsValid)
            {
                return View(movimento);
            }

            if (id != movimento.Id)
                return StatusCode(StatusCodes.Status400BadRequest);

            return View();
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
