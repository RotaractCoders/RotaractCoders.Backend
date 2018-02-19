using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

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
            _socioRepository.Excluir(id);

            return Ok();
        }
    }
}