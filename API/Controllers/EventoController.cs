using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Commands.Inputs;
using Infra.AzureTables;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/Evento")]
    public class EventoController : Controller
    {
        private EventoRepository _eventoRepository;

        public EventoController()
        {
            _eventoRepository = new EventoRepository();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Listar()
        {
            return Ok(_eventoRepository.Listar().OrderBy(x => x.DataEvento));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Buscar(string id)
        {
            return Ok(_eventoRepository.Obter(id));
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Incluir([FromBody]IncluirEventoInput input)
        {
            _eventoRepository.Incluir(new Evento(input));
            return Ok();
        }

        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Atualizar([FromBody]IncluirEventoInput input)
        {
            var evento = _eventoRepository.Obter(input.RowKey);

            evento.Atualizar(input);

            _eventoRepository.Atualizar(evento);
            return Ok();
        }

        [HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Deletar(string id)
        {
            var evento = _eventoRepository.Obter(id);

            if (evento == null)
                return BadRequest();

            evento.Inativar();
            _eventoRepository.Atualizar(evento);

            return Ok();
        }
    }
}
