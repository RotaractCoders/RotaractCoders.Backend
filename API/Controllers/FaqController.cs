using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/Faq")]
    public class FaqController : Controller
    {
        private FaqRepository _faqRepository;

        public FaqController()
        {
            _faqRepository = new FaqRepository();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Listar()
        {
            return Ok(_faqRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(string id)
        {
            return Ok(_faqRepository.Obter(id));
        }

        [HttpPost]
        public IActionResult Incluir([FromBody]IncluirFaqInput input)
        {
            var lista = _faqRepository.Listar();

            var posicao = 0;

            if (lista.Count > 0)
            {
                posicao = lista.Max(x => x.Posicao);
            }

            var faq = new Faq(input.Pergunta, input.Resposta, posicao + 1);
            _faqRepository.Incluir(faq);

            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody]IncluirFaqInput input)
        {
            var faq = _faqRepository.Obter(input.RowKey);

            faq.Atualizar(input.Pergunta, input.Resposta, input.Posicao);
            _faqRepository.Atualizar(faq);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Deletar(string id)
        {
            _faqRepository.Excluir(id);

            return Ok();
        }
    }
}