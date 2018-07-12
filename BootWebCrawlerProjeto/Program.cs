using Domain.Commands.Inputs;
using Domain.Commands.OmirBrasil.Results;
using Domain.Entities;
using Infra.AzureQueue;
using Infra.AzureTables;
using Infra.WebCrowley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BootWebCrawlerProjeto
{
    class Program
    {
        public static Processador processo { get; set; }

        static void Main(string[] args)
        {
            var processadorRepository = new ProcessadorRepository();
            processo = processadorRepository.BuscarProcessoEmAndamento();

            if (processo != null && (processo.StatusProcessamentoProjeto == "AguardandoProcessamento" || processo.StatusProcessamentoProjeto == "Processando"))
            {

                processo.IniciarProcessamentoProjeto();
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
                    var projetoQueue = new ProjetoQueue();
                    var quantidade = projetoQueue.QuantidadeProjetos();

                    Console.WriteLine($"Projetos processados {processo.QuantidadeProjetos - quantidade} de {processo.QuantidadeProjetos}");

                    if (quantidade == 0)
                    {
                        processo = processadorRepository.BuscarProcessoEmAndamento();

                        processo.FinalizarProcessamentoProjeto();
                        processadorRepository.Atualizar(processo);

                        break;
                    }

                    Thread.Sleep(10000);
                }
            }
        }

        static void NovaThread()
        {
            var queue = new ProjetoQueue();

            while (true)
            {
                using (var omir = new OmirBrasilProjetoRepository())
                {
                    var codigoProjeto = queue.ObterProximoProjeto();

                    if (codigoProjeto == null)
                        break;

                    var projetoOmir = omir.BuscarProjetoPorCodigo(codigoProjeto.AsString);
                    SalvarProjeto(projetoOmir);

                    queue.Deletar(codigoProjeto);
                }
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
