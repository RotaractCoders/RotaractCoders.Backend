﻿using Domain.Commands.Inputs;
using Domain.Commands.OmirBrasil.Results;
using Domain.Entities;
using Infra.AzureTables;
using Infra.WebCrowley;
using System.Collections.Generic;
using System.Linq;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var omir = new OmirBrasilRepository())
            {
                var clubes = omir.BuscarDistritoPorNumero("4430");

                clubes.CodigoClubes.ForEach(codigo =>
                {
                    var clube = omir.BuscarClubePorCodigo(codigo);
                    SalvarClube(clube);

                    clube.Socios.ForEach(socio =>
                    {
                        var socioOmir = omir.BuscarSocioPorCodigo(socio.Codigo);
                        SalvarSocio(socioOmir, socio.Email);
                    });
                });
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

        private static void SalvarSocio(OmirSocioResult socio, string email)
        {
            var socioRepository = new SocioRepository();
            var socioSalvo = socioRepository.ObterPorCodigo(socio.Codigo);

            var cargos = new List<CadastrarCargoSocioInput>();

            socio.CargosClube.ForEach(cargo =>
            {
                cargos.Add(new CadastrarCargoSocioInput
                {
                    Nome = cargo.NomeCargo,
                    De = cargo.De,
                    Ate = cargo.Ate,
                    TipoCargo = "Clube"
                });
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
            });

            if (socioSalvo == null)
            {
                socioRepository.Incluir(new Socio(new CadastroSocioInput()
                {
                    Codigo = socio.Codigo,
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
                    RowKey = socioSalvo.RowKey,
                    Codigo = socio.Codigo,
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
