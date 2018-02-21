using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Listar()
        {
            return Ok(_clubeRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult Obter(string id)
        {
            return Ok(_clubeRepository.Obter(id));
        }

        [HttpPost]
        public IActionResult Incluir([FromBody]CriarClubeInput input)
        {
            var clube = new Clube(input);

            _clubeRepository.Incluir(clube);

            return Ok();
        }

        [HttpPut]
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