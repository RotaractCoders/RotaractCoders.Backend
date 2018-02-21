using System;
using Microsoft.AspNetCore.Mvc;
using Infra.AzureTables;
using Microsoft.AspNetCore.Authorization;

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
                Clubes = _clubeRepository.Listar(dataUltimaAtualizacao),
                DadosEstaticos = _dadoEstaticoRepository.Listar(dataUltimaAtualizacao),
                Eventos = _eventoRepository.Listar(dataUltimaAtualizacao),
                Faqs = _faqRepository.Listar(dataUltimaAtualizacao),
                Socios = _socioRepository.Listar(dataUltimaAtualizacao),
                Data = DateTime.Now
            });
        }
    }
}