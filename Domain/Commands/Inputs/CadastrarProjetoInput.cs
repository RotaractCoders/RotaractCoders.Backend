using Domain.Contracts.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Commands.Inputs
{
    public class CadastrarProjetoInput : Notifiable, ICommand
    {
        public int Codigo { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public string Nome { get; set; }
        public string Justificativa { get; set; }
        public List<string> ObjetivosGerais { get; set; }
        public List<string> ObjetivosEspecificos { get; set; }
        public List<string> CategoriasPrincipais { get; set; }
        public List<string> CategoriasSecundarias { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public List<LancamentoFinanceiroInput> LancamentosFinanceiros { get; set; }
        public List<string> Participantes { get; set; }
        public List<string> PublicoAlvo { get; set; }
        public List<string> MeiosDeDivulgacao { get; set; }
        public List<string> Parcerias { get; set; }
        public List<TarefaInput> Tarefas { get; set; }
        public string Descricao { get; set; }
        public string Fotos { get; set; }
        public string Resultados { get; set; }
        public string Dificuldade { get; set; }
        public string PalavraChave { get; set; }
        public string LicoesAprendidas { get; set; }
        public string Resumo { get; set; }
        public int CodigoClube { get; set; }
        public string NomeClube { get; set; }
        public string NumeroDistrito { get; set; }
    }

    public class LancamentoFinanceiroInput
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }

    public class TarefaInput
    {
        public DateTime? Data { get; set; }
        public string Descricao { get; set; }
    }
}
