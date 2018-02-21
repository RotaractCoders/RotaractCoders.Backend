using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/Socio")]
    public class SocioController : Controller
    {
        private SocioRepository _socioRepository;

        public SocioController()
        {
            _socioRepository = new SocioRepository();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Listar()
        {
            return Ok(_socioRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult Obter(string id)
        {
            return Ok(_socioRepository.Obter(id));
        }

        [HttpPost]
        public IActionResult Incluir([FromBody] CadastroSocioInput input)
        {
            var socio = new Socio(input);
            _socioRepository.Incluir(socio);

            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] CadastroSocioInput input)
        {
            var socio = _socioRepository.Obter(input.RowKey);

            if (socio == null)
                return BadRequest();

            socio.Atualizar(input);
            _socioRepository.Atualizar(socio);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Deletar(string id)
        {
            var socio = _socioRepository.Obter(id);

            if (socio == null)
                return BadRequest();

            socio.Inativar();
            _socioRepository.Atualizar(socio);

            return Ok();
        }
    }
}