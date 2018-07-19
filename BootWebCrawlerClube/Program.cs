using Domain.Commands.Inputs;
using Domain.Commands.OmirBrasil.Results;
using Domain.Entities;
using Infra.AzureQueue;
using Infra.AzureTables;
using Infra.WebCrowley;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BootWebCrawlerClube
{
    class Program
    {
        public static Processador processo { get; set; }

        static void Main(string[] args)
        {
            var processadorRepository = new ProcessadorRepository();
            processo = processadorRepository.BuscarProcessoEmAndamento();

            if (processo != null && (processo.StatusProcessamentoClube == "AguardandoProcessamento" || processo.StatusProcessamentoClube == "Processando"))
            {
                processo.IniciarProcessamentoClube();
                processadorRepository.Atualizar(processo);

                var lista = new List<Thread>();

                lista.Add(new Thread(NovaThread));
                lista.Add(new Thread(NovaThread));
                lista.Add(new Thread(NovaThread));
                lista.Add(new Thread(NovaThread));
                lista.Add(new Thread(NovaThread));

                lista.ForEach(x =>
                {
                    x.Start();
                });

                while (true)
                {
                    var clubeQueue = new ClubeQueue();
                    var quantidade = clubeQueue.QuantidadeClubes();

                    Console.WriteLine($"Clubes processados {processo.QuantidadeClubes - quantidade} de {processo.QuantidadeClubes}");

                    if (quantidade == 0)
                    {
                        processo = processadorRepository.BuscarProcessoEmAndamento();

                        processo.FinalizarProcessamentoClube();
                        processadorRepository.Atualizar(processo);

                        break;
                    }

                    Thread.Sleep(10000);
                }
            }
        }

        static void NovaThread()
        {
            var clubeQueue = new ClubeQueue();

            while (true)
            {
                using (var omir = new OmirBrasilRepository())
                {
                    var codigoClube = clubeQueue.ObterProximoClube();

                    if (codigoClube == null)
                    {
                        break;
                    }

                    var clube = omir.BuscarClubePorCodigo(codigoClube.AsString);
                    SalvarClube(clube);

                    clubeQueue.Deletar(codigoClube);   
                }
            }
        }

        private static void SalvarClube(OmirClubeResult clube)
        {
            var clubeRepository = new ClubeRepository();
            var clubeSalvo = clubeRepository.ObterPorCodigo(clube.Codigo);

            if (clubeSalvo == null)
            {
                clubeRepository.Incluir(new Clube(new CriarClubeInput()
                {
                    Codigo = clube.Codigo,
                    Nome = clube.Nome,
                    DataFundacao = clube.DataFundacao,
                    Email = clube.Email,
                    NumeroDistrito = clube.NumeroDistrito,
                    Facebook = clube.Facebook,
                    RotaryPadrinho = clube.RotaryPadrinho,
                    DataFechamento = clube.DataFechamento,
                    Programa = "Rotaract",
                    Site = clube.Site
                }));
            }
            else
            {
                clubeSalvo.Atualizar(new CriarClubeInput()
                {
                    Codigo = clube.Codigo,
                    Nome = clube.Nome,
                    DataFundacao = clube.DataFundacao,
                    Email = clube.Email,
                    NumeroDistrito = clube.NumeroDistrito,
                    Facebook = clube.Facebook,
                    RotaryPadrinho = clube.RotaryPadrinho,
                    DataFechamento = clube.DataFechamento,
                    Programa = "Rotaract",
                    Site = clube.Site
                });

                clubeRepository.Atualizar(clubeSalvo);
            }
        }
    }
}
