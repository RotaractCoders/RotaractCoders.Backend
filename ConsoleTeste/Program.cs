using AngleSharp.Parser.Html;
using Domain.Commands.Handlers;
using Domain.Commands.Inputs;
using Infra.Repositories;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            var criarDistritoHandler = new CriarDistritoHandler(new DistritoRepository());
            var criarClubeHandler = new CriarClubeHandler(new ClubeRepository(), new DistritoRepository());
            var cadastrarSocioHandler = new CadastrarSocioHandler(new SocioRepository());
            var filiarSocioHandler = new FiliarSocioHandler(new DistritoRepository(), new SocioRepository(), new ClubeRepository(), new SocioClubeRepository());
            var cadastroCargoSocioHandler = new CadastroCargoSocioHandler(new CargoRepository(), new CargoClubeRepository(), new ClubeRepository(), new SocioRepository());
            var cadastroCargoDistritalHandler = new CadastroCargoDistritalHandler(new CargoRepository(), new SocioRepository(), new CargoDistritoRepository(), new DistritoRepository());
            var cadastroCargoRotaractBrasilHandler = new CadastroCargoRotaractBrasilHandler(new CargoRepository(), new SocioRepository(), new DistritoRepository(), new CargoRotaractBrasilRepository());

            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://www.omirbrasil.org.br/");
                driver.ExecuteScript("TrocaInclude('Sistema_OmirBrasil');");

                var distritos = new List<string>();
                var clubes = new List<Tuple<string, string>>();
                var socios = new List<Tuple<string, string>>();

                distritos = ExtrairNumeroDeTodosOsDistritos(driver.PageSource);

                distritos.Distinct().ToList().ForEach(numeroDistrito =>
                {
                    driver.ExecuteScript($"AbreFichaDistrito('{numeroDistrito}');");

                    var distritoInput = ExtratirDadosDistrito(driver.PageSource, numeroDistrito);
                    criarDistritoHandler.Handle(distritoInput);

                    clubes.AddRange(ExtrairCodigoDosClubesDoDistrito(driver.PageSource)
                        .Select(clube => new Tuple<string, string>(numeroDistrito, clube)).ToList());
                });

                clubes.Distinct().ToList().ForEach(clube =>
                {
                    driver.ExecuteScript($"javascript:AbreFichaClube('{clube.Item2}');");

                    var clubeInput = ExtratirDadosClube(driver.PageSource, Convert.ToInt32(clube.Item2), clube.Item1);
                    criarClubeHandler.Handle(clubeInput);

                    socios.AddRange(ExtrairCodigoDosSocios(driver.PageSource));
                });

                socios.Distinct().ToList().ForEach(socio =>
                {
                    driver.ExecuteScript($"javascript:AbreFichaSocio('{socio.Item1}');");

                    var socioInput = ExtratirDadosSocio(driver.PageSource, socio.Item2, Convert.ToInt32(socio.Item1));
                    cadastrarSocioHandler.Handle(socioInput);

                    //var filiacoesInput = ExtrairFilicoesDoSocio(driver.PageSource, socioInput.Codigo);
                    //filiarSocioHandler.Handle(filiacoesInput);

                    //var cargosSocioInput = ExtrairCargosDoSocioNosClubes(driver.PageSource, socioInput.Codigo);
                    //cadastroCargoSocioHandler.Handle(cargosSocioInput);

                    //var cargosDistritais = ExtrairCargosDoSocioDistritais(driver.PageSource, socioInput.Codigo);
                    //cadastroCargoDistritalHandler.Handle(cargosDistritais);

                    //var cargosRotaractBrasil = ExtrairCargosRotaractBrasilDoSocio(driver.PageSource, socioInput.Codigo);
                    //cadastroCargoRotaractBrasilHandler.Handle(cargosRotaractBrasil);
                });

                driver.Close();
                driver.Dispose();
            }
        }

        private static List<string> ExtrairNumeroDeTodosOsDistritos(string html)
        {
            var paginaDistrito = new HtmlParser().Parse(html);

            var listaDistritosHtml = paginaDistrito.QuerySelectorAll("#accordion tr")
                .Where(x => x.OuterHtml.Contains("javascript:AbreFichaDistrito"))
                .ToList();

            var distritos = new List<CriarDistritoInput>();

            return listaDistritosHtml
                .Select(x => x.QuerySelectorAll("strong").FirstOrDefault(a => a.InnerHtml.Contains("D.")).TextContent.Trim().Replace("D.", string.Empty))
                .ToList();
        }

        private static CriarDistritoInput ExtratirDadosDistrito(string htmlTexto, string numeroDistrito)
        {
            var html = new HtmlParser().Parse(htmlTexto);
            var htmlDadosDistrito = html.QuerySelector("#FichaSocio").TextContent;

            return new CriarDistritoInput
            {
                Numero = numeroDistrito,

                Mascote = htmlDadosDistrito.Split('\n')
                    .FirstOrDefault(x => x.Contains("Mascote:")).Replace("Mascote:", "").Trim(),

                Regiao = RomanoParaInteiro(htmlDadosDistrito.Split('\n')
                    .FirstOrDefault(x => x.Contains("Região:"))
                    .Replace("Região:", "").Trim()),

                Site = htmlDadosDistrito.Split('\n')
                    .FirstOrDefault(x => x.Contains("Site:")).Replace("Site:", "").Trim().ToLower(),

                Email = htmlDadosDistrito.Split('\n')
                    .FirstOrDefault(x => x.Contains("E-mail:")).Replace("E-mail:", "").Trim().ToLower()
            };
        }

        private static CriarClubeInput ExtratirDadosClube(string htmlTexto, int codigoClube, string numeroDistrito)
        {
            var html = new HtmlParser().Parse(htmlTexto);
            var htmlDadosClube = html.QuerySelector("#FichaSocio").TextContent;
            var htmlDadosPrincipaisClube = html.QuerySelectorAll("#Dados_Principais tr");

            var retorno = new CriarClubeInput
            {
                //Codigo = codigoClube,
                //numeroDistrito = numeroDistrito,
                Nome = htmlDadosClube.Substring(0, htmlDadosClube.IndexOf("D.")).Replace("\n", "").Trim(),
                RotaryPadrinho = htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("R.C Padrinho:")).Replace("R.C Padrinho:", "").Trim(),
                Site = htmlDadosPrincipaisClube.FirstOrDefault(x => x.TextContent.Contains("Site")).TextContent.Replace("\n", "").Replace("Site", "").Trim(),
                Email = htmlDadosPrincipaisClube.FirstOrDefault(x => x.TextContent.Contains("E-mail")).TextContent.Replace("\n", "").Replace("E-mail", "").Trim(),
                Facebook = htmlDadosPrincipaisClube.FirstOrDefault(x => x.TextContent.Contains("Facebook")).TextContent.Replace("\n", "").Replace("Facebook", "").Trim()
            };

            if (htmlDadosClube.Split('\n').FirstOrDefault(x => x.Contains("Data de Fechamento:")) != null)
            {
                retorno.DataFechamento = Convert.ToDateTime(htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("Data de Fechamento:")).Replace("Data de Fechamento:", "").Trim());
            }

            if (!string.IsNullOrEmpty(htmlDadosClube.Split('\n').FirstOrDefault(x => x.Contains("Data de Fundação:")).Replace("Data de Fundação:", "").Trim()))
            {
                retorno.DataFundacao = Convert.ToDateTime(htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("Data de Fundação:")).Replace("Data de Fundação:", "").Trim());
            }

            return retorno;
        }

        private static List<string> ExtrairCodigoDosClubesDoDistrito(string htmlTexto)
        {
            var html = new HtmlParser().Parse(htmlTexto);

            var codigoClubesAtivos = html
                        .QuerySelectorAll("#Guia_Clubes tr")
                        .Where(x => x.OuterHtml.Contains("javascript:AbreFichaClube"))
                        .Select(x =>
                        {
                            var texto = x.OuterHtml.Substring(x.OuterHtml.IndexOf("javascript:AbreFichaClube('"));

                            texto = texto.Replace("javascript:AbreFichaClube('", "");
                            texto = texto.Substring(0, texto.IndexOf("')"));

                            return texto;
                        });

            var codigoClubesInativos = html
                .QuerySelectorAll("#Guia_ClubesInativos tr")
                .Where(x => x.OuterHtml.Contains("javascript:AbreFichaClube"))
                .Select(x =>
                {
                    var texto = x.OuterHtml.Substring(x.OuterHtml.IndexOf("javascript:AbreFichaClube('"));

                    texto = texto.Replace("javascript:AbreFichaClube('", "");
                    texto = texto.Substring(0, texto.IndexOf("')"));

                    return texto;
                });

            var codigoClubes = new List<string>();

            codigoClubes.AddRange(codigoClubesAtivos);
            codigoClubes.AddRange(codigoClubesInativos);

            return codigoClubes;
        }

        private static List<Tuple<string, string>> ExtrairCodigoDosSocios(string htmlTexto)
        {
            var html = new HtmlParser().Parse(htmlTexto);

            var sociosAtivos = html.QuerySelectorAll("#FichaSocio tr")
                        .Where(x => x.OuterHtml.Contains("javascript:AbreFichaSocio"))
                        .Select(x =>
                        {
                            var texto = x.OuterHtml.Substring(x.OuterHtml.IndexOf("javascript:AbreFichaSocio('"));

                            texto = texto.Replace("javascript:AbreFichaSocio('", "");
                            var codigo = texto.Substring(0, texto.IndexOf("')"));

                            var email = string.Empty;

                            if (x.TextContent.Split('\n').FirstOrDefault(a => a.Contains("E-mail:")) != null)
                            {
                                email = x.TextContent
                                    .Split('\n')
                                    .FirstOrDefault(a => a.Contains("E-mail:")).Replace("E-mail:", "").Trim();
                            }

                            return new Tuple<string, string>(codigo, email);
                        });

            var sociosInativos = html
                .QuerySelectorAll("#Guia_ExMembros tr")
                .Where(x => x.OuterHtml.Contains("javascript:AbreFichaSocio"))
                .Select(x =>
                {
                    var texto = x.OuterHtml.Substring(x.OuterHtml.IndexOf("javascript:AbreFichaSocio('"));

                    texto = texto.Replace("javascript:AbreFichaSocio('", "");
                    var codigo = texto.Substring(0, texto.IndexOf("')"));

                    var email = string.Empty;

                    if (x.TextContent.Split('\n').FirstOrDefault(a => a.Contains("E-mail:")) != null)
                    {
                        email = x.TextContent
                            .Split('\n')
                            .FirstOrDefault(a => a.Contains("E-mail:")).Replace("E-mail:", "").Trim();
                    }

                    return new Tuple<string, string>(codigo, email);
                });

            var codigoSocios = new List<Tuple<string, string>>();

            codigoSocios.AddRange(sociosAtivos);
            codigoSocios.AddRange(sociosInativos);

            return codigoSocios;
        }

        private static CadastroSocioInput ExtratirDadosSocio(string htmlTexto, string email, int codigo)
        {
            var html = new HtmlParser().Parse(htmlTexto);
            var htmlDadosClube = html.QuerySelector("#FichaSocio").TextContent;

            var retorno = new CadastroSocioInput
            {
                Nome = htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("Nome:")).Replace("Nome:", "").Trim(),
                Apelido = htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("Apelido:")).Replace("Apelido:", "").Trim(),
                Email = email,
                //Codigo = codigo
            };

            if (!string.IsNullOrEmpty(htmlDadosClube.Split('\n').FirstOrDefault(x => x.Contains("Data de Nasc.:")).Replace("Data de Nasc.:", "").Trim()))
            {
                retorno.DataNascimento = Convert.ToDateTime(htmlDadosClube.Split('\n').FirstOrDefault(x => x.Contains("Data de Nasc.:")).Replace("Data de Nasc.:", "").Trim());
            }

            return retorno;
        }

        private static FiliarSocioListInput ExtrairFilicoesDoSocio(string htmlTexto, int codigoSocio)
        {
            var html = new HtmlParser().Parse(htmlTexto);

            var retorno = new FiliarSocioListInput
            {
                CodigoSocio = codigoSocio
            };

            retorno.Lista.AddRange(html.QuerySelectorAll("#Guia_Associacoes tr")
            .Where(x => x.ClassName != "SistemaLabel")
            .Select(x =>
            {
                var input = new FiliarSocioInput
                {
                    NumeroDistrito = x.QuerySelectorAll("td")[0].TextContent,
                    NomeClube = x.QuerySelectorAll("td")[1].TextContent
                };

                if (!string.IsNullOrEmpty(x.QuerySelectorAll("td")[2].TextContent.Trim()))
                {
                    input.Posse = Convert.ToDateTime(x.QuerySelectorAll("td")[2].TextContent);
                }
                else
                {
                    input.Posse = new DateTime(1900, 1, 1);
                }

                if (!string.IsNullOrEmpty(x.QuerySelectorAll("td")[3].TextContent))
                    input.Desligamento = Convert.ToDateTime(x.QuerySelectorAll("td")[3].TextContent);

                return input;
            }).ToList());

            return retorno;
        }

        private static CadastrarCargoSocioInput ExtrairCargosDoSocioNosClubes(string htmlTexto, int codigoSocio)
        {
            var html = new HtmlParser().Parse(htmlTexto);

            var input = new CadastrarCargoSocioInput
            {
                //CodigoSocio = codigoSocio
            };

            //input.Lista.AddRange(html.QuerySelectorAll("#Guia_CargosClube tr")
            //    .Where(x => x.ClassName != "SistemaLabel")
            //    .Select(x =>
            //    {
            //        var datas = x.QuerySelectorAll("td")[1].TextContent;
            //        var cargoClube = x.QuerySelectorAll("td")[0].TextContent;

            //        var retorno = new CargoSocioInput
            //        {
            //            Cargo = cargoClube.Substring(0, cargoClube.LastIndexOf("(")).Trim(),
            //            Clube = cargoClube.Substring(cargoClube.LastIndexOf("(")).Replace("(", "").Replace(")", "").Trim()
            //        };

            //        if (datas.Contains("até") && datas.Contains("desde"))
            //        {
            //            retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Substring(0, datas.Replace("desde", "").IndexOf("até")).Trim());
            //            retorno.Ate = Convert.ToDateTime(datas.Replace("desde", "").Substring(datas.Replace("desde", "").IndexOf("até")).Replace("até", "").Trim());
            //        }
            //        else if (datas.Contains("desde"))
            //        {
            //            retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Trim());
            //            retorno.Ate = null;
            //        }
            //        else if (datas.Contains("até"))
            //        {
            //            retorno.De = null;
            //            retorno.Ate = Convert.ToDateTime(datas.Replace("até", "").Trim());
            //        }
            //        else
            //        {
            //            retorno.De = null;
            //            retorno.Ate = null;
            //        }

            //        return retorno;
            //    }).ToList());

            return input;
        }

        //private static CadastroCargoDistritoInput ExtrairCargosDoSocioDistritais(string htmlTexto, int codigoSocio)
        private static string ExtrairCargosDoSocioDistritais(string htmlTexto, int codigoSocio)
        {
            //var html = new HtmlParser().Parse(htmlTexto);

            //var input = new CadastroCargoDistritoInput
            //{
            //    CodigoSocio = codigoSocio
            //};

            //input.Lista.AddRange(html.QuerySelectorAll("#Guia_CargosDistrito tr")
            //    .Where(x => x.ClassName != "SistemaLabel")
            //    .Select(x =>
            //    {
            //        var datas = x.QuerySelectorAll("td")[1].TextContent;
            //        var cargoDistrito = x.QuerySelectorAll("td")[0].TextContent;

            //        var retorno = new CargoDistritoInput
            //        {
            //            Cargo = cargoDistrito.Substring(0, cargoDistrito.LastIndexOf("(")).Trim(),
            //            Distrito = cargoDistrito.Substring(cargoDistrito.LastIndexOf("(")).Replace("(", "").Replace(")", "").Trim()
            //        };

            //        if (datas.Contains("até") && datas.Contains("desde"))
            //        {
            //            retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Substring(0, datas.Replace("desde", "").IndexOf("até")).Trim());
            //            retorno.Ate = Convert.ToDateTime(datas.Replace("desde", "").Substring(datas.Replace("desde", "").IndexOf("até")).Replace("até", "").Trim());
            //        }
            //        else if (datas.Contains("desde"))
            //        {
            //            retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Trim());
            //            retorno.Ate = null;
            //        }
            //        else if (datas.Contains("até"))
            //        {
            //            retorno.De = null;
            //            retorno.Ate = Convert.ToDateTime(datas.Replace("até", "").Trim());
            //        }
            //        else
            //        {
            //            retorno.De = null;
            //            retorno.Ate = null;
            //        }

            //        return retorno;
            //    }).ToList());

            //return input;

            return null;
        }

        //private static CadastroCargoRotaractBrasilInput ExtrairCargosRotaractBrasilDoSocio(string htmlTexto, int codigoSocio)
        private static string ExtrairCargosRotaractBrasilDoSocio(string htmlTexto, int codigoSocio)
        {
            //var html = new HtmlParser().Parse(htmlTexto);

            //var input = new CadastroCargoRotaractBrasilInput
            //{
            //    CodigoSocio = codigoSocio
            //};

            //input.Lista.AddRange(html.QuerySelectorAll("#Guia_CargosOmir tr")
            //    .Where(x => x.ClassName != "SistemaLabel")
            //    .Select(x =>
            //    {
            //        var datas = x.QuerySelectorAll("td")[1].TextContent;
            //        var cargo = x.QuerySelectorAll("td")[0].TextContent;

            //        var retorno = new CargoRotaractBrasilInput
            //        {
            //            Cargo = cargo.Trim()
            //        };

            //        if (datas.Contains("até") && datas.Contains("desde"))
            //        {
            //            retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Substring(0, datas.Replace("desde", "").IndexOf("até")).Trim());
            //            retorno.Ate = Convert.ToDateTime(datas.Replace("desde", "").Substring(datas.Replace("desde", "").IndexOf("até")).Replace("até", "").Trim());
            //        }
            //        else if (datas.Contains("desde"))
            //        {
            //            retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Trim());
            //            retorno.Ate = null;
            //        }
            //        else if (datas.Contains("até"))
            //        {
            //            retorno.De = null;
            //            retorno.Ate = Convert.ToDateTime(datas.Replace("até", "").Trim());
            //        }
            //        else
            //        {
            //            retorno.De = null;
            //            retorno.Ate = null;
            //        }

            //        return retorno;
            //    }).ToList());

            //return input;

            return null;
        }

        private static int RomanoParaInteiro(string numeroRomano)
        {
            switch (numeroRomano.ToUpper())
            {
                case "I": return 1;
                case "II": return 2;
                case "III": return 3;
                case "IV": return 4;
                case "V": return 5;
                case "VI": return 6;
                case "VII": return 7;
                case "VIII": return 8;
                case "IX": return 9;
                case "X": return 10;
                case "XI": return 11;
                default: return 0;
            }
        }
    }
}
