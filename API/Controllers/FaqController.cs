using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;

namespace API.Controllers
{
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
        public IActionResult Listar()
        {
            return Ok(_faqRepository.Listar());
        }

        [HttpPost]
        public IActionResult Incluir([FromBody]IncluirFaqInput input)
        {
            var lista = _faqRepository.Listar();

            var faq = new Faq(input.Pergunta, input.Resposta, lista.Max(x => x.Posicao) + 1);
            _faqRepository.Incluir(faq);

            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody]IncluirFaqInput input)
        {
            var faq = _faqRepository.Obter(input.id);

            faq.Atualizar(input.Pergunta, input.Resposta, input.Posicao);
            _faqRepository.Atualizar(faq);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(string id)
        {
            _faqRepository.Excluir(id);

            return Ok();
        }
    }
}