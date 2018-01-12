using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;

namespace API.Controllers
{
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
        public IActionResult Incluir(CriarClubeInput input)
        {
            var clube = new Clube(input);

            _clubeRepository.Incluir(clube);

            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar(CriarClubeInput input)
        {
            var clube = _clubeRepository.Obter(input.Id);

            if (clube == null)
                return BadRequest();

            clube.Atualizar(input);

            _clubeRepository.Atualizar(clube);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(string id)
        {
            _clubeRepository.Excluir(id);

            return Ok();
        }
    }
}