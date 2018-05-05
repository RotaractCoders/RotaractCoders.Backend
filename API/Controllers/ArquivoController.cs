using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/Arquivo")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ArquivoController : Controller
    {
        private ArquivoRepository _arquivoRepository;

        public ArquivoController()
        {
            _arquivoRepository = new ArquivoRepository();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Listar()
        {
            return Ok(_arquivoRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult Obter(string id)
        {
            return Ok(_arquivoRepository.Obter(id));
        }

        [HttpPost]
        public IActionResult Incluir([FromBody] IncluirArquivoInput input)
        {
            _arquivoRepository.Incluir(new Arquivo(input.Nome, input.Categoria, input.Link));

            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] IncluirArquivoInput input)
        {
            var arquivo = _arquivoRepository.Obter(input.RowKey);

            arquivo.Atualizar(input);
            _arquivoRepository.Atualizar(arquivo);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Deletar(string id)
        {
            var arquivo = _arquivoRepository.Obter(id);

            if (arquivo == null)
                return BadRequest();

            arquivo.Inativar();
            _arquivoRepository.Atualizar(arquivo);

            return Ok();
        }
    }
}