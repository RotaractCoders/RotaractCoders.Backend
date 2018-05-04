using Domain.Commands.Inputs;
using Domain.Commands.OmirBrasil.Results;
using Domain.Entities;
using Infra.AzureTables;
using Infra.WebCrowley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootWebCrawlerSocios
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var omir = new OmirBrasilRepository())
            {
                var clubes = omir.BuscarDistritoPorNumero("4430");

                for (int i = 0; i < clubes.CodigoClubes.Count; i++)
                {
                    Console.WriteLine($"Processando clube {i+1}/{clubes.CodigoClubes.Count}");

                    var clube = omir.BuscarClubePorCodigo(clubes.CodigoClubes[i]);
                    SalvarClube(clube);

                    for (int y = 0; y < clube.Socios.Count; y++)
                    {
                        Console.WriteLine($"Processando clube {i+1}/{clubes.CodigoClubes.Count} - Socio {y+1}/{clube.Socios.Count}");

                        var socioOmir = omir.BuscarSocioPorCodigo(clube.Socios[y].Codigo);
                        SalvarSocio(socioOmir, clube.Socios[y].Email, clubes.CodigoClubes[i], clube.NumeroDistrito);
                    }
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
                    NumeroDistrito = "4430",
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
                    NumeroDistrito = "4430",
                    Facebook = clube.Facebook,
                    RotaryPadrinho = clube.RotaryPadrinho,
                    DataFechamento = clube.DataFechamento,
                    Programa = "Rotaract",
                    Site = clube.Site
                });

                clubeRepository.Atualizar(clubeSalvo);
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

                cargoSocios.Add(new CargoSocio(cargo.NomeCargo, socio.Nome,"", socio.Codigo, numeroDistrito, "", socio.FotoUrl, "Rotaract Brasil", cargo.De, cargo.Ate, "Rotaract"));
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
