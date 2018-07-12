using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Infra.AzureTables.Consolidado;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Distrito")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class DistritoController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<string> { "4430" });
        }

        [HttpGet("Informacoes/{numeroDistrito}")]
        public IActionResult Informacoes(string numeroDistrito)
        {
            var distritoRepository = new DistritoRepository();

            var distrito = distritoRepository.Obter(numeroDistrito);

            if (distrito == null)
                return NotFound();

            return Ok(new
            {
                quantidadeSocios = distrito.QuantidadeSocios,
                quantidadeClubes = distrito.QuantidadeClubes
            });
        }
    }
}