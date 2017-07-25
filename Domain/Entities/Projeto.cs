using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Projeto
    {
        public Guid Id { get; private set; }
        public int Codigo { get; private set; }
        public DateTime? DataUltimaAtualizacao { get; private set; }
        public string Nome { get; private set; }
        public string Justificativa { get; private set; }
        public List<Objetivo> Objetivos { get; private set; }
        public List<ProjetoCategoria> ProjetoCategorias { get; private set; }
        public DateTime? DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }
        public DateTime? DataFinalizacao { get; private set; }
        public List<LancamentoFinanceiro> LancamentosFinanceiros { get; private set; }
        public List<string> Participantes { get; private set; }
        public List<string> PublicoAlvo { get; private set; }
        public List<string> MeiosDeDivulgacao { get; private set; }
        public List<string> Parcerias { get; private set; }
        public List<Tarefa> Tarefas { get; private set; }
        public string Descricao { get; private set; }
        public string Fotos { get; private set; }
        public string Resultados { get; private set; }
        public string Dificuldade { get; private set; }
        public string PalavraChave { get; private set; }
        public string LicoesAprendidas { get; private set; }
        public string Resumo { get; private set; }
        public Clube Clube { get; private set; }
    }
}
