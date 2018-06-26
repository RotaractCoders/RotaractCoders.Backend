using Domain.Commands.Inputs;
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
using System.Threading;

namespace BootWebCrawlerSocios
{
    class Program
    {
        public static Processador processo { get; set; }

        static void Main(string[] args)
        {
            var processadorRepository = new ProcessadorRepository();
            processo = processadorRepository.BuscarProcessoEmAndamento();

            if (processo != null && (processo.StatusProcessamentoSocio == "AguardandoProcessamento" || processo.StatusProcessamentoSocio == "Processando"))
            {

                processo.IniciarProcessamentoSocio();
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
                    var socioQueue = new SocioQueue();
                    var quantidadeSocio = socioQueue.QuantidadeSocios();

                    Console.WriteLine($"Socios processados {processo.QuantidadeSocios - quantidadeSocio} de {processo.QuantidadeSocios}");

                    if (quantidadeSocio == 0)
                    {
                        processo.FinalizarProcessamentoSocio();
                        processadorRepository.Atualizar(processo);

                        break;
                    }

                    Thread.Sleep(10000);
                }
            }
        }

        static void NovaThread()
        {
            var socioQueue = new SocioQueue();

            while (true)
            {
                var socioFila = socioQueue.ObterProximoSocio();

                if (socioFila == null)
                    break;

                var socio = JsonConvert.DeserializeObject<SocioQueueModel>(socioFila.AsString);

                using (var omir = new OmirBrasilRepository())
                {
                    var socioOmir = omir.BuscarSocioPorCodigo(socio.Codigo);
                    SalvarSocio(socioOmir, socio.Email, socio.CodigoClube, socio.NumeroDistrito);

                    socioQueue.Deletar(socioFila);
                }
            }
        }

        private static void SalvarSocio(OmirSocioResult socio, string email, string codigoClube, string numeroDistrito)
        {
            var socioRepository = new SocioRepository();
            var clubeRepository = new ClubeRepository();
            var cargoSocioRepository = new CargoSocioRepository();

            var lista = cargoSocioRepository.Listar(socio.Codigo);
            cargoSocioRepository.Excluir(lista);

            var socioSalvo = socioRepository.ObterPorCodigo(socio.Codigo, codigoClube);

            var cargos = new List<CadastrarCargoSocioInput>();
            var cargoSocios = new List<CargoSocio>();

            socio.CargosClube.ForEach(cargo =>
            {
                cargos.Add(new CadastrarCargoSocioInput
                {
                    Nome = cargo.NomeCargo,
                    De = cargo.De,
                    Ate = cargo.Ate,
                    TipoCargo = "Clube"
                });

                cargoSocios.Add(new CargoSocio(cargo.NomeCargo, socio.Nome, cargo.Clube, socio.Codigo, numeroDistrito, "", socio.FotoUrl, "Clube", cargo.De, cargo.Ate, "Rotaract"));
            });

            socio.CargosDistritais.ForEach(cargo =>
            {
                cargos.Add(new CadastrarCargoSocioInput
                {
                    Nome = cargo.NomeCargo,
                    De = cargo.De,
                    Ate = cargo.Ate,
                    TipoCargo = "Distrital"
                });

                cargoSocios.Add(new CargoSocio(cargo.NomeCargo, socio.Nome, "", socio.Codigo, numeroDistrito, cargo.Distrito, socio.FotoUrl, "Distrital", cargo.De, cargo.Ate, "Rotaract"));
            });

            socio.CargosRotaractBrasil.ForEach(cargo =>
            {
                cargos.Add(new CadastrarCargoSocioInput
                {
                    Nome = cargo.NomeCargo,
                    De = cargo.De,
                    Ate = cargo.Ate,
                    TipoCargo = "Rotaract Brasil"
                });

                cargoSocios.Add(new CargoSocio(cargo.NomeCargo, socio.Nome, "", socio.Codigo, numeroDistrito, "", socio.FotoUrl, "Rotaract Brasil", cargo.De, cargo.Ate, "Rotaract"));
            });

            cargoSocioRepository.Incluir(cargoSocios);

            if (socioSalvo == null)
            {
                socioRepository.Incluir(new Socio(new CadastroSocioInput()
                {
                    CodigoSocio = socio.Codigo,
                    CodigoClube = codigoClube,
                    Nome = socio.Nome,
                    Apelido = socio.Apelido,
                    DataNascimento = socio.DataNascimento,
                    Email = email,
                    Foto = socio.FotoUrl,
                    Cargos = cargos,
                    Clubes = socio.Clubes.Select(x => new CadastroSocioClubeInput
                    {
                        NomeClube = x.NomeClube,
                        Desligamento = x.Desligamento,
                        NumeroDistrito = x.NumeroDistrito,
                        Posse = x.Posse
                    }).ToList()
                }));
            }
            else
            {
                socioSalvo.Atualizar(new CadastroSocioInput()
                {
                    CodigoSocio = socio.Codigo,
                    CodigoClube = codigoClube,
                    Nome = socio.Nome,
                    Apelido = socio.Apelido,
                    DataNascimento = socio.DataNascimento,
                    Email = email,
                    Foto = socio.FotoUrl,
                    Cargos = cargos,
                    Clubes = socio.Clubes.Select(x => new CadastroSocioClubeInput
                    {
                        NomeClube = x.NomeClube,
                        Desligamento = x.Desligamento,
                        NumeroDistrito = x.NumeroDistrito,
                        Posse = x.Posse
                    }).ToList()
                });

                socioRepository.Atualizar(socioSalvo);
            }
        }
    }
}
