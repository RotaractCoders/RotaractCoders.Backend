using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Contracts.Repositories;
using Infra.Repositories;
using Domain.Entities;
using Domain.Commands.Inputs;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Evento")]
    public class EventoController : Controller
    {
        private IEventoRepository _eventoRepository;

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
        public IActionResult Buscar(Guid id)
        {
            return Ok(_eventoRepository.Buscar(id));
        }

        [HttpPost]
        public IActionResult Incluir(IncluirEventoInput input)
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
        public IActionResult Deletar(Guid id)
        {
            _eventoRepository.Deletar(id);
            return Ok();
        }
    }
}
