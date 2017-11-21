﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Contracts.Repositories;
using Infra.Repositories;
using Domain.Entities;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Evento")]
    public class EventoController : Controller
    {
        private ITipoEventoRepository _tipoEventoRepository;

        public EventoController()
        {
            _tipoEventoRepository = new TipoEventoRepository();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_tipoEventoRepository.Listar());
        }

        [HttpPost]
        public IActionResult Incluir(string descricao)
        {
            _tipoEventoRepository.Incluir(new TipoEvento(descricao));
            return Ok();
        }

        [HttpDelete]
        public IActionResult Deletar(Guid id)
        {
            _tipoEventoRepository.Deletar(id);

            return Ok();
        }
    }
}
