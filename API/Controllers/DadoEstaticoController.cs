using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/DadoEstatico")]
    public class DadoEstaticoController : Controller
    {
        private DadoEstaticoRepository _dadoEstaticoRepository;

        public DadoEstaticoController()
        {
            _dadoEstaticoRepository = new DadoEstaticoRepository();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_dadoEstaticoRepository.Listar());
        }

        [HttpGet("{nome}")]
        public IActionResult Buscar(string nome)
        {
            return Ok(_dadoEstaticoRepository.Obter(nome));
        }

        [HttpPost]
        public IActionResult Atualizar([FromBody] IncluirDadoEstaticoInput input)
        {
            var dadoEstatico = _dadoEstaticoRepository.Obter(input.Nome);

            if (dadoEstatico == null)
            {
                _dadoEstaticoRepository.Incluir(new DadoEstatico(input.Nome, input.Descricao));
                return Ok();
            }

            dadoEstatico.AtualizarDescricao(input.Descricao);
            _dadoEstaticoRepository.Atualizar(dadoEstatico);

            return Ok();
        }
    }
}