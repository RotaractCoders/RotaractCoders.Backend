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
        private CargoSocioRepository _cargoSocioRepository;
        private SocioBlob _socioBlob;

        public SocioController()
        {
            _socioRepository = new SocioRepository();
            _cargoSocioRepository = new CargoSocioRepository();
            _socioBlob = new SocioBlob();
        }

        [HttpGet("{codigoClube}")]
        [AllowAnonymous]
        public IActionResult Listar(string codigoClube)
        {
            return Ok(_socioRepository.Listar(codigoClube).Select(x=> new
            {
                x.Nome,
                x.CodigoSocio,
                x.Foto,
                x.CodigoClube,
                x.Clubes
            }));
        }

        [HttpGet("ListarEquipeDistrital/{gestaoInicio}/{gestaoFim}/{numeroDistrito}")]
        [AllowAnonymous]
        public IActionResult ListarEquipeDistrital(DateTime gestaoInicio, DateTime gestaoFim, string numeroDistrito)
        {
            return Ok(_cargoSocioRepository.ListarEquipeDistrital(gestaoInicio, gestaoFim, numeroDistrito, "Rotaract"));
        }

        [HttpGet("ListarPresidentes/{gestaoInicio}/{gestaoFim}/{numeroDistrito}")]
        [AllowAnonymous]
        public IActionResult ListarPresidentes(DateTime gestaoInicio, DateTime gestaoFim, string numeroDistrito)
        {
            return Ok(_cargoSocioRepository.Listar(gestaoInicio, gestaoFim, "Presidente", numeroDistrito, "Rotaract"));
        }

        [HttpGet("ListarRdrs/{numeroDistrito}")]
        [AllowAnonymous]
        public IActionResult ListarRdrs(string numeroDistrito)
        {
            return Ok(_cargoSocioRepository.Listar("Representante Distrital de Rotaract", numeroDistrito, "Rotaract"));
        }

        [HttpGet("obter/{codigoSocio}")]
        [AllowAnonymous]
        public IActionResult Obter(string codigoSocio)
        {
            return Ok(_socioRepository.ObterPorCodigo(codigoSocio));
        }

        [HttpGet("obter/{codigoSocio}/{codigoClube}")]
        [AllowAnonymous]
        public IActionResult Obter(string codigoSocio, string codigoClube)
        {
            return Ok(_socioRepository.ObterPorCodigo(codigoSocio, codigoClube));
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Incluir([FromBody] CadastroSocioInput input)
        {
            var socio = new Socio(input);
            return Ok(_socioRepository.Incluir(socio));
        }

        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Atualizar([FromBody] CadastroSocioInput input)
        {
            var socio = _socioRepository.ObterPorCodigo(input.CodigoSocio, input.CodigoClube);

            if (socio == null)
                return BadRequest();

            socio.Atualizar(input);
            _socioRepository.Atualizar(socio);

            return Ok();
        }

        [HttpPost("ImportarFoto")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Deletar(string codigoSocio, string codigoClube)
        {
            var socio = _socioRepository.ObterPorCodigo(codigoSocio, codigoClube);

            if (socio == null)
                return BadRequest();

            socio.Inativar();
            _socioRepository.Atualizar(socio);

            return Ok();
        }
    }
}