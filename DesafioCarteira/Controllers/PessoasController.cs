using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DesafioCarteira.Services;
using DesafioCarteira.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace DesafioCarteira.Controllers
{
    public class PessoasController : Controller
    {
        private readonly PessoasService _pessoasService;

        public PessoasController(PessoasService pessoasService) => _pessoasService = pessoasService;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Find()
        {
            var pessoas = await _pessoasService.FindAll();
            return Json(pessoas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            //Pessoa converted = JsonConvert.DeserializeObject<Pessoa>(pessoa);
            if (!ModelState.IsValid)
            {
                return View(pessoa);
            }
            pessoa.Saldo = 0;
            await _pessoasService.Add(pessoa);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            var pessoa = await _pessoasService.FindById(id.Value);
            if (pessoa == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return View(pessoa);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            var pessoa = await _pessoasService.FindById(id.Value);
            if (pessoa == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return View(pessoa);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return View(pessoa);
            }
            try
            {
                if (id != pessoa.Id)
                    return StatusCode(StatusCodes.Status400BadRequest);

                await _pessoasService.Update(pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // handle error
            }
            return View(pessoa);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                    throw new BadHttpRequestException("ID não pode ser nulo");

                await _pessoasService.Remove(id.Value);

                string url = Url.Action(nameof(Index));

                return Json(new { success = true, redirectUrl = url });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
