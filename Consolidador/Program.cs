using Domain.ConsolidadoEntities;
using Infra.AzureTables;
using Infra.AzureTables.Consolidado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolidador
{
    class Program
    {
        static void Main(string[] args)
        {
            var socioRepository = new SocioRepository();
            var clubeRepository = new ClubeRepository();
            var distritoRepository = new DistritoRepository();

            var numeroDistrito = "4430";

            var distrito = new Distrito(numeroDistrito, socioRepository.QuantidadeSocios(numeroDistrito), clubeRepository.QuantidadeClubes(numeroDistrito));

            distritoRepository.DeletarSeExistir(distrito.NumeroDistrito);
            distritoRepository.Incluir(distrito);
        }
    }
}
