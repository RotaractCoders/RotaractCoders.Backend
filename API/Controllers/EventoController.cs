using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Contracts.Repositories;
using Infra.Repositories;
using Domain.Entities;
using Domain.Commands.Inputs;
using Infra.AzureTables;

namespace API.Controllers
{
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
        public IActionResult Listar()
        {
            return Ok(_eventoRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(string id)
        {
            return Ok(_eventoRepository.Buscar(id));
        }

        [HttpPost]
        public IActionResult Incluir([FromBody]IncluirEventoInput input)
        {
            _eventoRepository.Incluir(new Evento(input));
            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar(AtualizarEventoInput input)
        {
            var evento = _eventoRepository.Obter(input.Id);

            evento.Atualizar(input);

            _eventoRepository.Atualizar(evento);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Deletar(string id)
        {
            _eventoRepository.Deletar(id);
            return Ok();
        }
    }
}
