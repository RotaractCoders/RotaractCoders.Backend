using Domain.Commands.Inputs;
using Infra.AzureTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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

        [HttpGet("{codigoClube}")]
        [AllowAnonymous]
        public IActionResult Listar(string codigoClube)
        {
            return Ok(_projetoRepository.Listar(codigoClube).Select(x => new
            {
                x.Nome,
                x.Codigo,
                x.Descricao,
                x.Resumo
            }));
        }

        [HttpGet("obter/{codigoProjeto}")]
        [AllowAnonymous]
        public IActionResult Obter(string codigoProjeto)
        {
            return Ok(_projetoRepository.ObterPorCodigo(codigoProjeto));
        }
    }
}