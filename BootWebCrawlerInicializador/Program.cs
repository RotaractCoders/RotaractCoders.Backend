using Domain.Commands.OmirBrasil.Results;
using Domain.Commands.QueueModel;
using Domain.Entities;
using Infra.AzureQueue;
using Infra.AzureTables;
using Infra.WebCrowley;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BootWebCrawlerInicializador
{
    class Program
    {
        static void Main(string[] args)
        {
            var clubeQueue = new ClubeQueue();
            var socioQueue = new SocioQueue();
            var projetoQueue = new ProjetoQueue();

            var listaClubes = new List<string>();
            var listaSocios = new List<SocioQueueModel>();
            var listaProjetos = new List<string>();

            var processo = new ProcessadorRepository().BuscarProcessoEmAndamento();
            if (processo != null) return;

            var numeroDistrito = "4430";

            using (var omir = new OmirBrasilRepository())
            {
                var distrito = omir.BuscarDistritoPorNumero(numeroDistrito);

                //distrito.CodigoClubes = distrito.CodigoClubes.Take(2).ToList();

                listaClubes.AddRange(distrito.CodigoClubes);

                foreach (var codigoClube in distrito.CodigoClubes)
                {
                    var clube = omir.BuscarClubePorCodigo(codigoClube);

                    listaSocios.AddRange(clube.Socios.Select(x => new SocioQueueModel
                    {
                        Codigo = x.Codigo,
                        Email = x.Email,
                        CodigoClube = codigoClube,
                        NumeroDistrito = distrito.Numero
                    }));
                }
            }

            using (var omir = new OmirBrasilProjetoRepository())
            {
                var codigosProjetos = omir.ListarCodigoProjetosPorDistrito(numeroDistrito);

                listaProjetos.AddRange(codigosProjetos);
            }

            var processador = new Processador(
                "",
                listaClubes.Count,
                listaSocios.Count,
                listaProjetos.Count);

            var processadorRepository = new ProcessadorRepository();
            processadorRepository.Incluir(processador);

            foreach (var codigoClube in listaClubes)
            {
                clubeQueue.Incluir(codigoClube);
            }

            foreach (var socio in listaSocios)
            {
                socioQueue.Incluir(JsonConvert.SerializeObject(socio));
            }

            foreach (var codigoProjeto in listaProjetos)
            {
                projetoQueue.Incluir(codigoProjeto);
            }
        }
    }
}
