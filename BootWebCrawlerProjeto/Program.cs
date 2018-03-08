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

namespace BootWebCrawlerProjeto
{
    class Program
    {
        static void Main(string[] args)
        {
            var omir = new OmirBrasilProjetoRepository();

            var lista = omir.ListarCodigoProjetosPorDistrito("4430");

            for (int i = 0; i < lista.Count; i++)
            {
                var projetoOmir = omir.BuscarProjetoPorCodigo(lista[i]);
                SalvarProjeto(projetoOmir);

                Console.WriteLine($"{i}/{lista.Count}");
            }
        }

        private static void SalvarProjeto(OmirProjetoResult projetoOmir)
        {
            var projetoRepository = new ProjetoRepository();
            var projetoSalvo = projetoRepository.ObterPorCodigo(projetoOmir.Codigo.ToString());

            if (projetoSalvo == null)
            {
                var projeto = new Projeto(new CadastrarProjetoInput
                {
                    CategoriasPrincipais = projetoOmir.CategoriasPrincipais,
                    CategoriasSecundarias = projetoOmir.CategoriasSecundarias,
                    Codigo = projetoOmir.Codigo,
                    CodigoClube = projetoOmir.CodigoClube,
                    DataFim = projetoOmir.DataFim,
                    DataFinalizacao = projetoOmir.DataFinalizacao,
                    DataInicio = projetoOmir.DataInicio,
                    DataUltimaAtualizacao = projetoOmir.DataUltimaAtualizacao,
                    Descricao = projetoOmir.Descricao,
                    Dificuldade = projetoOmir.Dificuldade,
                    Fotos = projetoOmir.Fotos,
                    Justificativa = projetoOmir.Justificativa,
                    LicoesAprendidas = projetoOmir.LicoesAprendidas,
                    MeiosDeDivulgacao = projetoOmir.MeiosDeDivulgacao,
                    Nome = projetoOmir.Nome,
                    NomeClube = projetoOmir.NomeClube,
                    NumeroDistrito = projetoOmir.NumeroDistrito,
                    ObjetivosEspecificos = projetoOmir.ObjetivosEspecificos,
                    ObjetivosGerais = projetoOmir.ObjetivosGerais,
                    PalavraChave = projetoOmir.PalavraChave,
                    Parcerias = projetoOmir.Parcerias,
                    Participantes = projetoOmir.Participantes,
                    PublicoAlvo = projetoOmir.PublicoAlvo,
                    Resultados = projetoOmir.Resultados,
                    Resumo = projetoOmir.Resumo,
                    LancamentosFinanceiros = projetoOmir.LancamentosFinanceiros
                    .Select(x => new LancamentoFinanceiroInput
                    {
                        Data = x.Data,
                        Descricao = x.Descricao,
                        Valor = x.Valor
                    }).ToList(),
                    Tarefas = projetoOmir.Tarefas
                    .Select(x => new TarefaInput
                    {
                        Data = x.Data,
                        Descricao = x.Descricao
                    }).ToList()
                });

                projetoRepository.Incluir(projeto);
            }
            else
            {
                projetoSalvo.Atualizar(new CadastrarProjetoInput
                {
                    CategoriasPrincipais = projetoOmir.CategoriasPrincipais,
                    CategoriasSecundarias = projetoOmir.CategoriasSecundarias,
                    Codigo = projetoOmir.Codigo,
                    CodigoClube = projetoOmir.CodigoClube,
                    DataFim = projetoOmir.DataFim,
                    DataFinalizacao = projetoOmir.DataFinalizacao,
                    DataInicio = projetoOmir.DataInicio,
                    DataUltimaAtualizacao = projetoOmir.DataUltimaAtualizacao,
                    Descricao = projetoOmir.Descricao,
                    Dificuldade = projetoOmir.Dificuldade,
                    Fotos = projetoOmir.Fotos,
                    Justificativa = projetoOmir.Justificativa,
                    LicoesAprendidas = projetoOmir.LicoesAprendidas,
                    MeiosDeDivulgacao = projetoOmir.MeiosDeDivulgacao,
                    Nome = projetoOmir.Nome,
                    NomeClube = projetoOmir.NomeClube,
                    NumeroDistrito = projetoOmir.NumeroDistrito,
                    ObjetivosEspecificos = projetoOmir.ObjetivosEspecificos,
                    ObjetivosGerais = projetoOmir.ObjetivosGerais,
                    PalavraChave = projetoOmir.PalavraChave,
                    Parcerias = projetoOmir.Parcerias,
                    Participantes = projetoOmir.Participantes,
                    PublicoAlvo = projetoOmir.PublicoAlvo,
                    Resultados = projetoOmir.Resultados,
                    Resumo = projetoOmir.Resumo,
                    LancamentosFinanceiros = projetoOmir.LancamentosFinanceiros
                    .Select(x => new LancamentoFinanceiroInput
                    {
                        Data = x.Data,
                        Descricao = x.Descricao,
                        Valor = x.Valor
                    }).ToList(),
                    Tarefas = projetoOmir.Tarefas
                    .Select(x => new TarefaInput
                    {
                        Data = x.Data,
                        Descricao = x.Descricao
                    }).ToList()
                });

                projetoRepository.Atualizar(projetoSalvo);
            }
        }
    }
}
