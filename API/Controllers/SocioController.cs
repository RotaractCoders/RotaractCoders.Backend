using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Domain.Commands.Inputs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Infra.AzureBlobs;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/Socio")]
    public class SocioController : Controller
    {
        private SocioRepository _socioRepository;
        private SocioBlob _socioBlob;

        public SocioController()
        {
            _socioRepository = new SocioRepository();
            _socioBlob = new SocioBlob();
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
            return Ok(_socioRepository.Incluir(socio));
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

        [HttpPost("ImportarFoto")]
        [AllowAnonymous]
        public async Task<IActionResult> ImportarFoto(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var nomeFoto = Guid.NewGuid().ToString();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = formFile.OpenReadStream())
                    {
                        return Ok(_socioBlob.SalvarFotoPerfil(nomeFoto + ".jpg", stream));
                    }
                }
            }

            return BadRequest();
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