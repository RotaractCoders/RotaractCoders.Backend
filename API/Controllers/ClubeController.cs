using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/Clube")]
    public class ClubeController : Controller
    {
        private ClubeRepository _clubeRepository;

        public ClubeController()
        {
            _clubeRepository = new ClubeRepository();
        }

        [HttpGet("{numeroDistrito}")]
        [AllowAnonymous]
        public IActionResult Listar(string numeroDistrito)
        {
            var lista = _clubeRepository.Listar(numeroDistrito)
                .Select(x => new { x.Codigo, x.Nome, x.DataFechamento });

            return Ok(lista);
        }

        [HttpGet("obter/{codigo}")]
        [AllowAnonymous]
        public IActionResult Obter(string codigo)
        {
            return Ok(_clubeRepository.Obter(codigo));
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Incluir([FromBody]CriarClubeInput input)
        {
            var clube = new Clube(input);

            _clubeRepository.Incluir(clube);

            return Ok();
        }

        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Atualizar([FromBody]CriarClubeInput input)
        {
            var clube = _clubeRepository.Obter(input.RowKey);

            if (clube == null)
                return BadRequest();

            clube.Atualizar(input);

            _clubeRepository.Atualizar(clube);

            return Ok();
        }

        [HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Deletar(string id)
        {
            var clube = _clubeRepository.Obter(id);

            if (clube == null)
                return BadRequest();

            clube.Inativar();
            _clubeRepository.Atualizar(clube);

            return Ok();
        }
    }
}