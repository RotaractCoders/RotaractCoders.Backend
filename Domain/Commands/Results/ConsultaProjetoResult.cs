using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Results
{
    public class ConsultaProjetoResult
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public string Nome { get; set; }
        public string Justificativa { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public string Descricao { get; set; }
        public string Fotos { get; set; }
        public string Resultados { get; set; }
        public string Dificuldade { get; set; }
        public string PalavraChave { get; set; }
        public string LicoesAprendidas { get; set; }
        public string Resumo { get; set; }
        public string Clube { get; set; }
        public string Distrito { get; set; }

        public List<ConsultaProjetoLancamentoFinanceiroResult> LancamentosFinanceiros { get; private set; }
        public List<string> Participantes { get; private set; }
        public List<string> PublicoAlvo { get; private set; }
        public List<string> MeiosDeDivulgacao { get; private set; }
        public List<string> Parcerias { get; private set; }
        public List<ConsultaProjetoTarefaResult> Tarefas { get; private set; }
        public List<ConsultaProjetoObjetivoResult> Objetivos { get; private set; }
        public List<ConsultaProjetoCategoriaResult> ProjetoCategorias { get; private set; }

        public ConsultaProjetoResult(Projeto projeto)
        {
            Id = projeto.Id;
            Codigo = projeto.Codigo;
            DataUltimaAtualizacao = projeto.DataUltimaAtualizacao;
            Nome = projeto.Nome;
            Justificativa = projeto.Justificativa;
            DataInicio = projeto.DataInicio;
            DataFim = projeto.DataFim;
            DataFinalizacao = projeto.DataFinalizacao;
            Descricao = projeto.Descricao;
            Fotos = projeto.Fotos;
            Resultados = projeto.Resultados;
            Dificuldade = projeto.Dificuldade;
            PalavraChave = projeto.PalavraChave;
            LicoesAprendidas = projeto.LicoesAprendidas;
            Resumo = projeto.Resumo;
            Participantes = projeto.Participantes.Select(x => x.Descricao).ToList();
            PublicoAlvo = projeto.PublicoAlvo.Select(x => x.Descricao).ToList();
            MeiosDeDivulgacao = projeto.MeiosDeDivulgacao.Select(x => x.Descricao).ToList();
            Parcerias = projeto.Parcerias.Select(x => x.Descricao).ToList();
            Clube = projeto.Clube.Nome;
            //Distrito = projeto.Clube.Distrito.Numero;

            LancamentosFinanceiros = projeto.LancamentosFinanceiros
                .Select(x => new ConsultaProjetoLancamentoFinanceiroResult
                {
                    Data = x.Data,
                    Descricao = x.Descricao,
                    Valor = x.Valor
                }).ToList();

            Tarefas = projeto.Tarefas
                .Select(x => new ConsultaProjetoTarefaResult
                {
                    Data = x.Data,
                    Descricao = x.Descricao
                }).ToList();

            Tarefas = projeto.Tarefas
                .Select(x => new ConsultaProjetoTarefaResult
                {
                    Data = x.Data,
                    Descricao = x.Descricao
                }).ToList();

            Objetivos = projeto.Objetivos
                .Select(x => new ConsultaProjetoObjetivoResult
                {
                    Descricao = x.Descricao,
                    TipoObjetivo = x.TipoObjetivo.ToString()
                }).ToList();

            ProjetoCategorias = projeto.ProjetoCategorias
                .Select(x => new ConsultaProjetoCategoriaResult
                {
                    Descricao = x.Categoria.Nome,
                    TipoCategoria = x.TipoCategoria.ToString()
                }).ToList();
        }
    }

    public class ConsultaProjetoLancamentoFinanceiroResult
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }

    public class ConsultaProjetoTarefaResult
    {
        public DateTime? Data { get; set; }
        public string Descricao { get; set; }
    }

    public class ConsultaProjetoObjetivoResult
    {
        public string TipoObjetivo { get; set; }
        public string Descricao { get; set; }
    }

    public class ConsultaProjetoCategoriaResult
    {
        public string TipoCategoria { get; set; }
        public string Descricao { get; set; }
    }
}
