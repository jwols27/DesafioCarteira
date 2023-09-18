using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioCarteira.Services;

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
    }
}
