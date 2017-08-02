using AngleSharp;
using AngleSharp.Dom;
using Domain.Commands.Inputs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Infra.WebCrowley
{
    public class OmirBrasilRepository
    {
        private IConfiguration config;

        public OmirBrasilRepository()
        {
            config = Configuration.Default.WithDefaultLoader();
        }

        public CadastrarProjetoInput GetByCode(int code)
        {
            var omirBrasilUrl = $"http://projetos.omirbrasil.org.br/exibe_projetos.php?ID_PROJETO={code}";

            var document = BrowsingContext.New(config).OpenAsync(omirBrasilUrl).Result;

            if (document.Body.TextContent.Contains("Projeto não encontrado!"))
            {
                return null;
            }

            var title = document.QuerySelectorAll(".Titulo");
            var tableTr = document.QuerySelectorAll("#projetoprincipal tr");

            var simpleFields = tableTr.Where(x => x.QuerySelectorAll("b").Length > 0).ToList();

            return new CadastrarProjetoInput
            {
                Codigo = code,
                Nome = GetProjectName(title),
                Justificativa = GetProjectRationale(simpleFields),
                ObjetivosGerais = GetProjectGeneralObjective(simpleFields),
                ObjetivosEspecificos = GetProjectSpecificObjective(simpleFields),
                CategoriasPrincipais = GetProjectMainCategory(simpleFields),
                CategoriasSecundarias = GetProjectOtherCategories(simpleFields),
                DataInicio = GetProjectStartDate(simpleFields),
                DataFim = GetProjectEndDate(simpleFields),
                DataFinalizacao = GetProjectCompletionDate(simpleFields),
                LancamentosFinanceiros = GetProjectProjectFinancials(simpleFields),
                Participantes = ExtrairParticipantes(simpleFields),
                PublicoAlvo = ExtrairPublicoAlvo(simpleFields),
                MeiosDeDivulgacao = ExtrairMeiosDeDivilgacao(simpleFields),
                Parcerias = GetProjectPartnerships(simpleFields),
                Tarefas = GetProjectSchedule(simpleFields),
                Descricao = GetProjectDescription(simpleFields),
                Resultados = GetProjectResults(simpleFields),
                Dificuldade = GetProjectDifficulty(simpleFields),
                PalavraChave = GetProjectKeyWords(simpleFields),
                Resumo = GetProjectSummary(simpleFields),
                DataUltimaAtualizacao = ExtrairDataUltimaAtualizacao(simpleFields),
                Fotos = ExtrairFoto(simpleFields),
                LicoesAprendidas = ExtrairLicoesAprendidas(simpleFields),
                CodigoClube = GetClubCode(document),
                NomeClube = GetClubName(title),
                NumeroDistrito = GetDistrictNumber(title)
            };
        }

        public void Dispose()
        {
            config = null;
        }

        #region GetByCode - private methods

        private static string ExtrairLicoesAprendidas(List<IElement> listFields)
        {
            return GetValueOfListFields(listFields, "Lições Aprendidas")
                .FirstOrDefault();
        }

        private static string ExtrairFoto(List<IElement> listFields)
        {
            return GetValueOfSimpleField(listFields, "Fotos do Projeto");
        }

        private static DateTime? ExtrairDataUltimaAtualizacao(List<IElement> simpleFields)
        {
            DateTime data;
            if (DateTime.TryParse(GetValueOfSimpleField(simpleFields, "Data Hora Cadastro / Alterado em"), out data))
            {
                return data;
            }

            return null;
        }

        private static string GetProjectSummary(List<IElement> listFields)
        {
            return GetValueOfListFields(listFields, "Resumo")
                .FirstOrDefault();
        }

        private static string GetProjectKeyWords(List<IElement> listFields)
        {
            return GetValueOfSimpleField(listFields, "Palavras-Chave");
        }

        private static string GetProjectDifficulty(List<IElement> listFields)
        {
            return GetValueOfSimpleField(listFields, "Grau de Dificuldade");
        }

        private static string GetProjectResults(List<IElement> listFields)
        {
            return GetValueOfListFields(listFields, "Resultados Qualitativos e Quantitativos")
                .FirstOrDefault();
        }

        private static string GetProjectDescription(List<IElement> listFields)
        {
            return GetValueOfListFields(listFields, "Descrição")
                .FirstOrDefault();
        }

        private static List<TarefaInput> GetProjectSchedule(List<IElement> listFields)
        {
            var element = listFields
                .FirstOrDefault(x => x.QuerySelector("b")
                .InnerHtml
                .Contains("Cronograma de Atividades"))
                .NextElementSibling
                .QuerySelectorAll("tr")
                .ToList();

            return element.Select(x =>
            {
                return new TarefaInput
                {
                    Data = TryParseDateTime(x.Children[0].TextContent),
                    Descricao = x.Children[1].TextContent.Trim()
                };
            }).ToList();
        }

        private static DateTime? TryParseDateTime(string texto)
        {
            DateTime data;

            if (DateTime.TryParse(texto, out data))
            {
                return data;
            }

            return null;
        }

        private static List<string> GetProjectPartnerships(List<IElement> listFields)
        {
            return GetValueOfListFields(listFields, "Parcerias");
        }

        private static List<string> ExtrairMeiosDeDivilgacao(List<IElement> listFields)
        {
            return GetValueOfListFields(listFields, "Meios de Divulgação")
                .Select(x => x.Split(';')[0].Trim())
                .ToList();
        }

        private static List<string> ExtrairPublicoAlvo(List<IElement> listFields)
        {
            var result = GetValueOfSimpleField(listFields, "Público Alvo")
                .Split('-')
                .Where(x => x != string.Empty)
                .Select(x => x
                    .Replace("-", string.Empty)
                    .Trim());

            return result
                .ToList();
        }

        private static List<string> ExtrairParticipantes(List<IElement> listFields)
        {
            return GetValueOfSimpleField(listFields, "Quem trabalho no Projeto | Ação")
                .Split('-')
                .Select(x => RemoverNumeros(x).Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
        }

        private static string RemoverNumeros(string texto)
        {
            var retorno = new System.Text.StringBuilder();

            foreach (var item in texto)
            {
                if (!char.IsNumber(item))
                    retorno.Append(item);
            }

            return retorno.ToString();
        }

        private static List<LancamentoFinanceiroInput> GetProjectProjectFinancials(List<IElement> listFields)
        {
            var element = listFields
                .FirstOrDefault(x => x.QuerySelector("b")
                .InnerHtml
                .Contains("Relatório Financeiro"))
                .NextElementSibling
                .QuerySelectorAll("tr");

            var result = new List<LancamentoFinanceiroInput>();

            for (int i = 0; i < element.Count() - 1; i++)
            {
                result.Add(new LancamentoFinanceiroInput
                {
                    Data = Convert.ToDateTime(element[i].Children[0].TextContent),
                    Descricao = element[i].Children[1].TextContent.Trim(),
                    Valor = Convert.ToDecimal(element[i].Children[2].TextContent.Trim().Replace(".", ""))
                });
            }

            return result;
        }

        private static DateTime? GetProjectCompletionDate(List<IElement> listFields)
        {
            var result = new DateTime();

            if (DateTime.TryParse(GetValueOfSimpleField(listFields, "Data Finalização"), out result))
                return result;

            return null;
        }

        private static DateTime? GetProjectEndDate(List<IElement> listFields)
        {
            return TryParseDateTime(GetValueOfSimpleField(listFields, "Data Fim"));
        }

        private static DateTime? GetProjectStartDate(List<IElement> listFields)
        {
            return TryParseDateTime(GetValueOfSimpleField(listFields, "Data Início"));
        }

        private static List<string> GetProjectOtherCategories(List<IElement> listFields)
        {
            var category = GetValueOfSimpleField(listFields, "Outras Categorias");

            return new List<string>
            {
                category
            };
        }

        private static List<string> GetProjectMainCategory(List<IElement> listFields)
        {
            var category = GetValueOfSimpleField(listFields, "Categoria Principal");

            return new List<string>
            {
                category
            };
        }

        private static string GetProjectRationale(List<IElement> listFields)
        {
            return GetValueOfListFields(listFields, "Justificativa")
                .FirstOrDefault();
        }

        private static List<string> GetProjectGeneralObjective(List<IElement> listFields)
        {
            return GetValueOfListFields(listFields, "Objetivo Geral");
        }

        private static List<string> GetProjectSpecificObjective(List<IElement> listFields)
        {
            return GetValueOfListFields(listFields, "Objetivo Especifíco")
                .Where(x => x != string.Empty)
                .ToList();
        }

        private static string GetProjectName(IHtmlCollection<IElement> title)
        {
            return title[0]
                .InnerHtml
                .Split('>')[1]
                .Trim();
        }

        private static string GetDistrictNumber(IHtmlCollection<IElement> title)
        {
            return title[1]
                            .InnerHtml
                            .Substring(title[1].InnerHtml.LastIndexOf('.'))
                            .Replace(".", string.Empty)
                            .Trim();
        }

        private static int GetClubCode(IDocument document)
        {
            return int.Parse(document
                            .QuerySelectorAll("#projetoprincipal tr a")
                            .FirstOrDefault(x => x.TextContent.Contains("Clique aqui para exibir a ficha completa do clube."))
                            .Attributes.FirstOrDefault(x => x.Name == "href")
                            .Value.Split('\'')[1]);
        }



        private static string GetClubEmail(List<IElement> simpleFields)
        {
            return GetValueOfSimpleField(simpleFields, "E-mail do Clube");
        }

        private static string GetValueOfSimpleField(List<IElement> simpleFields, string bText)
        {
            var element = simpleFields
                            .FirstOrDefault(x => x.QuerySelector("b")
                            .InnerHtml
                            .Contains(bText));

            if (element == null) return string.Empty;

            var result = element.TextContent
                .Substring(element.TextContent.IndexOf(':'))
                .Replace("\n", string.Empty)
                .Trim();

            result = new Regex(Regex.Escape(":"))
                .Replace(result, string.Empty, 1)
                .Trim();

            result = new Regex(Regex.Escape("-"))
                .Replace(result, string.Empty, 1)
                .Trim();

            return result;
        }

        private static List<string> GetValueOfListFields(List<IElement> simpleFields, string bText)
        {
            var element = simpleFields
                .FirstOrDefault(x => x.QuerySelector("b")
                .InnerHtml
                .Contains(bText));

            if (element == null) return new List<string>();

            return element.NextElementSibling
                .QuerySelectorAll("p")
                .Select(x => x.TextContent.Trim())
                .Select(x => x.StartsWith("-") ? x.Substring(1).Trim() : x.Trim())
                .ToList();
        }

        private static string GetClubName(IHtmlCollection<IElement> title)
        {
            return title[1]
                            .InnerHtml
                            .Substring(0, title[1].InnerHtml.LastIndexOf('-'))
                            .Trim();
        }

        #endregion
    }
}
