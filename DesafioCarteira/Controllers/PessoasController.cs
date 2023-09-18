using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioCarteira.Services;
using DesafioCarteira.Models;
using System.Globalization;
using System.Text.Json;
using Newtonsoft.Json;

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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string pessoa)
        {
            Pessoa converted = JsonConvert.DeserializeObject<Pessoa>(pessoa);
            return Json(converted);
        }
    }
}
