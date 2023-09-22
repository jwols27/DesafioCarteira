using DesafioCarteira.Models;
using DesafioCarteira.Models.ViewModels;
using DesafioCarteira.Services;
using DesafioCarteira.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioCarteira.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly MovimentacoesService _movimentacoesService;

        public MovimentacoesController(MovimentacoesService movimentacoesService) =>
            _movimentacoesService = movimentacoesService;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Get(int? id, string type, string before, string after)
        {
            try
            {
                if (id == null)
                    throw new BadHttpRequestException("ID não pode ser nulo");

                DateTime? beforeData = null, afterData = null;

                IEnumerable<Movimentacao> temp;

                if (DateTime.TryParse(before, out DateTime result))
                    beforeData = result;

                if (DateTime.TryParse(after, out result))
                    afterData = result;

                switch (type)
                {
                    case "E":
                        temp = await _movimentacoesService.FindAllById<Entrada>(id.Value, beforeData, afterData);
                        break;

                    case "S":
                        temp = await _movimentacoesService.FindAllById<Saida>(id.Value, beforeData, afterData);
                        break;

                    default:
                        temp = await _movimentacoesService.FindAllById(id.Value, beforeData, afterData);
                        break;
                }

                IEnumerable<TypedMov> movs = temp.Select(mov => {
                    if (mov is Entrada)
                        return new TypedMov("E", mov);
                    if (mov is Saida)
                        return new TypedMov("S", mov);
                    throw new InvalidOperationException("Tipo inesperado: " + mov.GetType());
                });

                return Json(new { movs });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<IActionResult> Details(int? id, int? pessoaId, string type)
        {
            try
            {
                if (id == null || pessoaId == null)
                    throw new BadHttpRequestException("ID não pode ser nulo");

                Movimentacao mov;

                switch (type)
                {
                    case "E":
                        mov = await _movimentacoesService.FindById<Entrada>(id.Value, pessoaId.Value);
                        break;

                    case "S":
                        mov = await _movimentacoesService.FindById<Saida>(id.Value, pessoaId.Value);
                        break;

                    default:
                        throw new InvalidOperationException("Tipo inesperado: " + type);
                }

                if (mov == null)
                    throw new NotFoundException("Movimentação não encontrada");

                return View(new TypedMov(type, mov));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<IActionResult> Edit(int? id, int pessoaId, string type)
        {
            try
            {
                if (id == null)
                    throw new BadHttpRequestException("ID não pode ser nulo");

                Movimentacao mov;
                switch (type)
                {
                    case "E":
                        mov = await _movimentacoesService.FindById<Entrada>(id.Value, pessoaId);
                        break;
                    case "S":
                        mov = await _movimentacoesService.FindById<Saida>(id.Value, pessoaId);
                        break;
                    default:
                        throw new InvalidOperationException("Tipo inesperado: " + type);
                }
                if (mov == null)
                    throw new NotFoundException("Movimentação não encontrada");

                return View(new TypedMov(type, mov));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string mov, string type)
        {
            Movimentacao converted;
            try
            {
                switch (type)
                {
                    case "E":
                        converted = JsonConvert.DeserializeObject<Entrada>(mov);
                        break;
                    case "S":
                        converted = JsonConvert.DeserializeObject<Saida>(mov);
                        break;
                    default:
                        throw new InvalidOperationException("Tipo inesperado: " + type);
                }

                if (id != converted.Id)
                    throw new BadHttpRequestException("Discrepância de IDs");

                int pessoaId = converted.Pessoa.Id;

                await _movimentacoesService.Update(converted, pessoaId);

                string url = Url.Action(nameof(Index));

                return Json(new { redirectUrl = url });
            }
            catch (Exception)
            {
                converted = JsonConvert.DeserializeObject<Movimentacao>(mov);
                return View(new TypedMov(type, converted));
            }

        }

        public async Task<IActionResult> Delete(int? id, int? pessoaId, string type)
        {
            try
            {
                if (id == null || pessoaId == null)
                    throw new BadHttpRequestException("ID não pode ser nulo");

                switch (type)
                {
                    case "E":
                        await _movimentacoesService.Remove<Entrada>(id.Value, pessoaId.Value);
                        break;

                    case "S":
                        await _movimentacoesService.Remove<Saida>(id.Value, pessoaId.Value);
                        break;

                    default:
                        throw new InvalidOperationException("Tipo inesperado: " + type);
                }

                string url = Url.Action(nameof(Index));

                return Json(new { redirectUrl = url });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
