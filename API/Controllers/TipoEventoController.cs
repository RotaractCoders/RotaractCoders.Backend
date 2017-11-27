using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Contracts.Repositories;
using Infra.Repositories;
using Domain.Entities;
using Domain.Commands.Inputs;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/TipoEvento")]
    public class TipoEventoController : Controller
    {
        private ITipoEventoRepository _tipoEventoRepository;

        public TipoEventoController()
        {
            _tipoEventoRepository = new TipoEventoRepository();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_tipoEventoRepository.Listar());
        }

        [HttpPost]
        public IActionResult Incluir([FromBody] IncluirTipoEventoInput input)
        {
            _tipoEventoRepository.Incluir(new TipoEvento(input.Descricao));
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