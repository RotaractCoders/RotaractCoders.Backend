using System;
using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Consolidado")]
    public class ConsolidadoController : Controller
    {
        private ArquivoRepository _arquivoRepository;
        private ClubeRepository _clubeRepository;
        private DadoEstaticoRepository _dadoEstaticoRepository;
        private EventoRepository _eventoRepository;
        private FaqRepository _faqRepository;
        private SocioRepository _socioRepository;
        private ProjetoRepository _projetoRepository;

        public ConsolidadoController()
        {
            _arquivoRepository = new ArquivoRepository();
            _clubeRepository = new ClubeRepository();
            _dadoEstaticoRepository = new DadoEstaticoRepository();
            _eventoRepository = new EventoRepository();
            _faqRepository = new FaqRepository();
            _socioRepository = new SocioRepository();
        }

        [HttpGet("{dataUltimaAtualizacao}")]
        [AllowAnonymous]
        public IActionResult Listar(DateTime dataUltimaAtualizacao)
        {
            return Ok(new
            {
                Arquivos = _arquivoRepository.Listar(dataUltimaAtualizacao),
                DadosEstaticos = _dadoEstaticoRepository.Listar(dataUltimaAtualizacao),
                Eventos = _eventoRepository.Listar(dataUltimaAtualizacao),
                Faqs = _faqRepository.Listar(dataUltimaAtualizacao),
                Data = DateTime.Now
            });
        }

        [HttpGet("listarClubes")]
        [AllowAnonymous]
        public IActionResult ListarClubes()
        {
            return Ok(_clubeRepository.Listar("4430").Select(x=> x.Codigo).ToList());
        }

        [HttpGet("clube/{codigoClube}/{dataUltimaAtualizacao}")]
        [AllowAnonymous]
        public IActionResult Listar(string codigoClube, DateTime dataUltimaAtualizacao)
        {
            return Ok(new
            {
                Clubes = _clubeRepository.Listar(dataUltimaAtualizacao, codigoClube),
                Socios = _socioRepository.Listar(dataUltimaAtualizacao, codigoClube)
                //Projetos = _projetoRepository.Listar(dataUltimaAtualizacao, codigoClube)
            });
        }
    }
}