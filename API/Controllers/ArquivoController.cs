using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Arquivo")]
    public class ArquivoController : Controller
    {
        private ArquivoRepository _arquivoRepository;

        public ArquivoController()
        {
            _arquivoRepository = new ArquivoRepository();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_arquivoRepository.Listar());
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
            var arquivo = _arquivoRepository.Obter(input.Id);

            arquivo.Atualizar(input.Nome, input.Categoria, input.Link);
            _arquivoRepository.Atualizar(arquivo);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(string id)
        {
            _arquivoRepository.Excluir(id);

            return Ok();
        }
    }
}