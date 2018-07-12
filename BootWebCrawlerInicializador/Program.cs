using Domain.Commands.OmirBrasil.Results;
using Domain.Commands.QueueModel;
using Domain.Entities;
using Infra.AzureQueue;
using Infra.AzureTables;
using Infra.WebCrowley;
using Newtonsoft.Json;
using System;
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

            Console.WriteLine("Processando distrito: " + numeroDistrito);

            using (var omir = new OmirBrasilRepository())
            {
                var distrito = omir.BuscarDistritoPorNumero(numeroDistrito);
                
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

                    Console.WriteLine("Clube: " + clube.Nome + " processado");
                }
            }

            Console.WriteLine("iniciando listagem dos projetos");

            using (var omir = new OmirBrasilProjetoRepository())
            {
                var codigosProjetos = omir.ListarCodigoProjetosPorDistrito(numeroDistrito);

                listaProjetos.AddRange(codigosProjetos);
            }

            Console.WriteLine("finalizado listagem dos projetos");

            var processador = new Processador(
                numeroDistrito,
                listaClubes.Count,
                listaSocios.Count,
                listaProjetos.Count);

            var processadorRepository = new ProcessadorRepository();
            processadorRepository.Incluir(processador);

            Console.WriteLine("carregando filas");

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
