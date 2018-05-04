using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize("Bearer")]
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
        [AllowAnonymous]
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

        [HttpDelete]
        public IActionResult Deletar(string id)
        {
            var dadoEstatico = _dadoEstaticoRepository.Obter(id);

            if (dadoEstatico == null)
                return BadRequest();

            dadoEstatico.Inativar();
            _dadoEstaticoRepository.Atualizar(dadoEstatico);

            return Ok();
        }
    }
}