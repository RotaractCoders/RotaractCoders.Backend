using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using Domain.Commands.Inputs;
using Domain.Commands.OmirBrasil.Results;
using Domain.Entities;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Infra.WebCrowley
{
    public class OmirBrasilRepository : IDisposable
    {
        private IConfiguration config;
        private ChromeDriver driver;

        public OmirBrasilRepository()
        {
            config = Configuration.Default.WithDefaultLoader();
            driver = new ChromeDriver("C:/");
            driver.Navigate().GoToUrl("http://www.omirbrasil.org.br/");
        }

        public List<string> ListarDistritos()
        {
            driver.ExecuteScript("TrocaInclude('Sistema_OmirBrasil');");

            return ExtrairNumeroDeTodosOsDistritos(driver.PageSource);
        }

        public OmirDistritoResult BuscarDistritoPorNumero(string numeroDistrito)
        {
            driver.ExecuteScript($"AbreFichaDistrito('{numeroDistrito}');");

            return ExtrairDadosDistrito(driver.PageSource, numeroDistrito);
        }

        public OmirClubeResult BuscarClubePorCodigo(string codigo)
        {
            driver.ExecuteScript($"AbreFichaClube('{codigo}');");

            return ExtratirDadosClube(driver.PageSource, codigo);
        }

        public OmirSocioResult BuscarSocioPorCodigo(string codigo)
        {
            driver.ExecuteScript($"AbreFichaSocio('{codigo}');");

            return ExtratirDadosSocio(driver.PageSource, codigo);
        }

        #region ListarDistritos - privcate methods

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

        #endregion

        #region BuscarDistritoPorNumero - private methods

        private static OmirDistritoResult ExtrairDadosDistrito(string htmlTexto, string numeroDistrito)
        {
            var html = new HtmlParser().Parse(htmlTexto);
            var htmlDadosDistrito = html.QuerySelector("#FichaSocio").TextContent;

            return new OmirDistritoResult
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
                    .FirstOrDefault(x => x.Contains("E-mail:")).Replace("E-mail:", "").Trim().ToLower(),

                CodigoClubes = ExtrairCodigoDosClubesDoDistrito(htmlTexto)
            };
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

        #endregion

        #region BuscarClubePorCodigo - private methods

        private static OmirClubeResult ExtratirDadosClube(string htmlTexto, string codigo)
        {
            var html = new HtmlParser().Parse(htmlTexto);
            var htmlDadosClube = html.QuerySelector("#FichaSocio").TextContent;
            var htmlDadosPrincipaisClube = html.QuerySelectorAll("#Dados_Principais tr");

            var retorno = new OmirClubeResult
            {
                Codigo = codigo,
                Nome = htmlDadosClube.Substring(0, htmlDadosClube.IndexOf("D.")).Replace("\n", "").Trim(),
                RotaryPadrinho = htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("R.C Padrinho:")).Replace("R.C Padrinho:", "").Trim(),
                Site = htmlDadosPrincipaisClube.FirstOrDefault(x => x.TextContent.Contains("Site")).TextContent.Replace("\n", "").Replace("Site", "").Trim(),
                Email = htmlDadosPrincipaisClube.FirstOrDefault(x => x.TextContent.Contains("E-mail")).TextContent.Replace("\n", "").Replace("E-mail", "").Trim(),
                Facebook = htmlDadosPrincipaisClube.FirstOrDefault(x => x.TextContent.Contains("Facebook")).TextContent.Replace("\n", "").Replace("Facebook", "").Trim(),
                Socios = ExtrairCodigoDosSocios(htmlTexto)
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

        private static List<OmirClubeSocioResult> ExtrairCodigoDosSocios(string htmlTexto)
        {
            var html = new HtmlParser().Parse(htmlTexto);

            var sociosAtivos = html
                .QuerySelectorAll("#Guia_QS tr")
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

                    return new OmirClubeSocioResult
                    {
                        Codigo = codigo,
                        Email = email
                    };
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

                    return new OmirClubeSocioResult
                    {
                        Codigo = codigo,
                        Email = email
                    };
                });

            var codigoSocios = new List<OmirClubeSocioResult>();

            codigoSocios.AddRange(sociosAtivos);
            codigoSocios.AddRange(sociosInativos);

            return codigoSocios;
        }

        #endregion

        #region BuscarSocioProCodigo - private methods

        private static OmirSocioResult ExtratirDadosSocio(string htmlTexto, string codigo)
        {
            var html = new HtmlParser().Parse(htmlTexto);
            var htmlDadosClube = html.QuerySelector("#FichaSocio").TextContent;

            var retorno = new OmirSocioResult
            {
                Codigo = codigo,
                FotoUrl = "http://www.omirbrasil.org.br/" + html.QuerySelector(".Foto").OuterHtml
                    .Replace("<img class=\"Foto\" src=\"", "")
                    .Replace("\">", "")
                    .Split('?')[0],
                Nome = htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("Nome:")).Replace("Nome:", "").Trim(),
                Apelido = htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("Apelido:")).Replace("Apelido:", "").Trim(),
                ClubeAtual = htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("Clube:")).Replace("Clube:", "").Trim(),
                Clubes = ExtrairFilicoesDoSocio(htmlTexto, codigo),
                CargosClube = ExtrairCargosDoSocioNosClubes(htmlTexto),
                CargosDistritais = ExtrairCargosDoSocioDistritais(htmlTexto),
                CargosRotaractBrasil = ExtrairCargosRotaractBrasilDoSocio(htmlTexto)
            };

            if (!string.IsNullOrEmpty(htmlDadosClube.Split('\n').FirstOrDefault(x => x.Contains("Data de Nasc.:")).Replace("Data de Nasc.:", "").Trim()))
            {
                retorno.DataNascimento = Convert.ToDateTime(htmlDadosClube.Split('\n').FirstOrDefault(x => x.Contains("Data de Nasc.:")).Replace("Data de Nasc.:", "").Trim());
            }

            return retorno;
        }

        private static List<OmirSocioClubeResult> ExtrairFilicoesDoSocio(string htmlTexto, string codigoSocio)
        {
            var html = new HtmlParser().Parse(htmlTexto);

            var retorno = new List<OmirSocioClubeResult>();

            retorno.AddRange(html.QuerySelectorAll("#Guia_Associacoes tr")
            .Where(x => x.ClassName != "SistemaLabel")
            .Select(x =>
            {
                var input = new OmirSocioClubeResult
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

        private static List<OmirSocioCargoClubeResult> ExtrairCargosDoSocioNosClubes(string htmlTexto)
        {
            var html = new HtmlParser().Parse(htmlTexto);

            var input = new List<OmirSocioCargoClubeResult>();

            input.AddRange(html.QuerySelectorAll("#Guia_CargosClube tr")
                .Where(x => x.ClassName != "SistemaLabel")
                .Select(x =>
                {
                    var datas = x.QuerySelectorAll("td")[1].TextContent;
                    var cargoClube = x.QuerySelectorAll("td")[0].TextContent;

                    var retorno = new OmirSocioCargoClubeResult
                    {
                        NomeCargo = cargoClube.Substring(0, cargoClube.LastIndexOf("(")).Trim(),
                        Clube = cargoClube.Substring(cargoClube.LastIndexOf("(")).Replace("(", "").Replace(")", "").Trim()
                    };

                    if (datas.Contains("até") && datas.Contains("desde"))
                    {
                        retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Substring(0, datas.Replace("desde", "").IndexOf("até")).Trim());
                        retorno.Ate = Convert.ToDateTime(datas.Replace("desde", "").Substring(datas.Replace("desde", "").IndexOf("até")).Replace("até", "").Trim());
                    }
                    else if (datas.Contains("desde"))
                    {
                        retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Trim());
                        retorno.Ate = null;
                    }
                    else if (datas.Contains("até"))
                    {
                        retorno.De = null;
                        retorno.Ate = Convert.ToDateTime(datas.Replace("até", "").Trim());
                    }
                    else
                    {
                        retorno.De = null;
                        retorno.Ate = null;
                    }

                    return retorno;
                }).ToList());

            return input;
        }

        private static List<OmirSocioCargoDistritalResult> ExtrairCargosDoSocioDistritais(string htmlTexto)
        {
            var html = new HtmlParser().Parse(htmlTexto);

            var input = new List<OmirSocioCargoDistritalResult>();
            
            input.AddRange(html.QuerySelectorAll("#Guia_CargosDistrito tr")
                .Where(x => x.ClassName != "SistemaLabel")
                .Select(x =>
                {
                    var datas = x.QuerySelectorAll("td")[1].TextContent;
                    var cargoDistrito = x.QuerySelectorAll("td")[0].TextContent;

                    var retorno = new OmirSocioCargoDistritalResult
                    {
                        NomeCargo = cargoDistrito.Substring(0, cargoDistrito.LastIndexOf("(")).Trim(),
                        Distrito = cargoDistrito.Substring(cargoDistrito.LastIndexOf("(")).Replace("(", "").Replace(")", "").Trim()
                    };

                    if (datas.Contains("até") && datas.Contains("desde"))
                    {
                        retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Substring(0, datas.Replace("desde", "").IndexOf("até")).Trim());
                        retorno.Ate = Convert.ToDateTime(datas.Replace("desde", "").Substring(datas.Replace("desde", "").IndexOf("até")).Replace("até", "").Trim());
                    }
                    else if (datas.Contains("desde"))
                    {
                        retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Trim());
                        retorno.Ate = null;
                    }
                    else if (datas.Contains("até"))
                    {
                        retorno.De = null;
                        retorno.Ate = Convert.ToDateTime(datas.Replace("até", "").Trim());
                    }
                    else
                    {
                        retorno.De = null;
                        retorno.Ate = null;
                    }

                    return retorno;
                }).ToList());

            return input;
        }

        private static List<OmirSocioCargoRotaractBrasilResult> ExtrairCargosRotaractBrasilDoSocio(string htmlTexto)
        {
            var html = new HtmlParser().Parse(htmlTexto);

            var input = new List<OmirSocioCargoRotaractBrasilResult>();

            input.AddRange(html.QuerySelectorAll("#Guia_CargosOmir tr")
                .Where(x => x.ClassName != "SistemaLabel")
                .Select(x =>
                {
                    var datas = x.QuerySelectorAll("td")[1].TextContent;
                    var cargo = x.QuerySelectorAll("td")[0].TextContent;

                    var retorno = new OmirSocioCargoRotaractBrasilResult
                    {
                        NomeCargo = cargo.Trim()
                    };

                    if (datas.Contains("até") && datas.Contains("desde"))
                    {
                        retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Substring(0, datas.Replace("desde", "").IndexOf("até")).Trim());
                        retorno.Ate = Convert.ToDateTime(datas.Replace("desde", "").Substring(datas.Replace("desde", "").IndexOf("até")).Replace("até", "").Trim());
                    }
                    else if (datas.Contains("desde"))
                    {
                        retorno.De = Convert.ToDateTime(datas.Replace("desde", "").Trim());
                        retorno.Ate = null;
                    }
                    else if (datas.Contains("até"))
                    {
                        retorno.De = null;
                        retorno.Ate = Convert.ToDateTime(datas.Replace("até", "").Trim());
                    }
                    else
                    {
                        retorno.De = null;
                        retorno.Ate = null;
                    }

                    return retorno;
                }).ToList());

            return input;
        } 

        #endregion

        public void Dispose()
        {
            config = null;
            driver.Close();
            driver.Dispose();
        }
    }
}
