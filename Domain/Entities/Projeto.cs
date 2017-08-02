using Domain.Commands.Inputs;
using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Projeto : Entity
    {
        public int Codigo { get; private set; }
        public DateTime? DataUltimaAtualizacao { get; private set; }
        public string Nome { get; private set; }
        public string Justificativa { get; private set; }
        public DateTime? DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }
        public DateTime? DataFinalizacao { get; private set; }
        public string Descricao { get; private set; }
        public string Fotos { get; private set; }
        public string Resultados { get; private set; }
        public string Dificuldade { get; private set; }
        public string PalavraChave { get; private set; }
        public string LicoesAprendidas { get; private set; }
        public string Resumo { get; private set; }
        public Guid IdClube { get; private set; }

        public Clube Clube { get; private set; }
        public List<LancamentoFinanceiro> LancamentosFinanceiros { get; private set; }
        public List<ParticipanteProjeto> Participantes { get; private set; }
        public List<PublicoAlvoProjeto> PublicoAlvo { get; private set; }
        public List<MeioDeDivulgacaoProjeto> MeiosDeDivulgacao { get; private set; }
        public List<ParceriaProjeto> Parcerias { get; private set; }
        public List<Tarefa> Tarefas { get; private set; }
        public List<Objetivo> Objetivos { get; private set; }
        public List<ProjetoCategoria> ProjetoCategorias { get; private set; }

        protected Projeto()
        {

        }

        public Projeto(CadastrarProjetoInput command, Guid idClube)
        {
            Codigo = command.Codigo;
            DataUltimaAtualizacao = command.DataUltimaAtualizacao;
            Nome = command.Nome;
            Justificativa = command.Justificativa;
            DataInicio = command.DataInicio;
            DataFim = command.DataFim;
            DataFinalizacao = command.DataFinalizacao;
            Descricao = command.Descricao;
            Fotos = command.Fotos;
            Resultados = command.Resultados;
            Dificuldade = command.Dificuldade;
            PalavraChave = command.PalavraChave;
            LicoesAprendidas = command.LicoesAprendidas;
            Resumo = command.Resumo;
            IdClube = idClube;
        }

        public void Atualizar(CadastrarProjetoInput command)
        {
            DataUltimaAtualizacao = command.DataUltimaAtualizacao;
            Nome = command.Nome;
            Justificativa = command.Justificativa;
            DataInicio = command.DataInicio;
            DataFim = command.DataFim;
            DataFinalizacao = command.DataFinalizacao;
            Descricao = command.Descricao;
            Fotos = command.Fotos;
            Resultados = command.Resultados;
            Dificuldade = command.Dificuldade;
            PalavraChave = command.PalavraChave;
            LicoesAprendidas = command.LicoesAprendidas;
            Resumo = command.Resumo;
        }
    }
}
