using Domain.Commands.Inputs;
using Infra.AzureTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/Projeto")]
    public class ProjetoController : Controller
    {
        private ProjetoRepository _projetoRepository;

        public ProjetoController()
        {
            _projetoRepository = new ProjetoRepository();
        }

        [HttpGet("{idProjeto}")]
        public IActionResult Buscar(Guid idProjeto)
        {
            return Ok(_projetoRepository.Obter(idProjeto.ToString()));
        }

        [HttpGet("lista")]
        public IActionResult Listar([FromHeader]ListaProjetosInput input)
        {
            //return Ok(_projetoRepository.Listar(input));
            return Ok();
        }
    }
}