using DesafioCarteira.Models;
using DesafioCarteira.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DesafioCarteira.Controllers
{
    public class CarteiraController : Controller
    {
        private readonly MovimentoService _movimentoService;

        public CarteiraController(MovimentoService movimentoService) => _movimentoService = movimentoService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEntrada(string movimento)
        {
            try
            {
                Entrada entrada = JsonConvert.DeserializeObject<Entrada>(movimento);

                if (entrada.Pessoa == null)
                    throw new BadHttpRequestException("Uma pessoa deve estar selecionada");

                if (entrada.Valor < 0)
                    entrada.Valor = entrada.Valor * -1;

                await _movimentoService.Add(entrada);

                return Json(new { pessoaId = entrada.Pessoa.Id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSaida(string movimento)
        {
            try
            {
                Saida saida = JsonConvert.DeserializeObject<Saida>(movimento);

                if (saida.Pessoa == null)
                    throw new BadHttpRequestException("Uma pessoa deve estar selecionada");

                if (saida.Valor > 0)
                    saida.Valor = saida.Valor * -1;

                await _movimentoService.Add(saida);

                return Json(new { pessoaId = saida.Pessoa.Id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
