using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;

namespace API.Controllers
{
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
        public IActionResult Listar()
        {
            return Ok(_socioRepository.Listar());
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
            var socio = _socioRepository.Obter(input.Id);

            if (socio == null)
                return BadRequest();

            socio.Atualizar(input);
            _socioRepository.Atualizar(socio);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(string id)
        {
            _socioRepository.Excluir(id);

            return Ok();
        }
    }
}