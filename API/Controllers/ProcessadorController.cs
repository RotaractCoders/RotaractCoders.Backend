using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Infra.AzureTables;
using Infra.AzureQueue;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Processador")]
    public class ProcessadorController : Controller
    {
        [HttpGet("ProcessoEmAndamento")]
        [AllowAnonymous]
        public IActionResult ObterProcessoEmAndamento()
        {
            var processador = new ProcessadorRepository();
            var filaClube = new ClubeQueue();
            var filaSocio = new SocioQueue();
            var filaProjeto = new ProjetoQueue();

            var processo = processador.BuscarProcessoEmAndamento();

            if (processador == null)
                return Ok();

            return Ok(new
            {
                processo = processo,
                quantidadeClubeFila = filaClube.QuantidadeClubes(),
                quantidadeSociosFila = filaSocio.QuantidadeSocios(),
                quantidadeProjetosFila = filaProjeto.QuantidadeProjetos()
            });
        }

        [HttpGet("ListarProcessamentoFinalizados")]
        [AllowAnonymous]
        public IActionResult ListarProcessamentosFinalizados()
        {
            var processadorRepository = new ProcessadorRepository();
            
            return Ok(processadorRepository.ListarProcessamentosFinalizados());
        }
    }
}