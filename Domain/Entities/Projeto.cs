using Domain.Commands.Inputs;
using Domain.Entities.Base;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Projeto : TableEntity
    {
        #region Properties

        public string Codigo { get; set; }
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
        public string CodigoClube { get; set; }
        public string NomeClube { get; set; }
        public string NumeroDistrito { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public string ObjetivosGeraisSerializado { get; set; }
        public List<string> ObjetivosGerais
        {
            get
            {
                if (ObjetivosGeraisSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(ObjetivosGeraisSerializado);
            }
        }

        public string ObjetivosEspecificosSerializado { get; set; }
        public List<string> ObjetivosEspecificos
        {
            get
            {
                if (ObjetivosEspecificosSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(ObjetivosEspecificosSerializado);
            }
        }

        public string CategoriasPrincipaisSerializado { get; set; }
        public List<string> CategoriasPrincipais
        {
            get
            {
                if (CategoriasPrincipaisSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(CategoriasPrincipaisSerializado);
            }
        }


        public string CategoriasSecundariasSerializado { get; set; }
        public List<string> CategoriasSecundarias
        {
            get
            {
                if (CategoriasSecundariasSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(CategoriasSecundariasSerializado);
            }
        }

        public string ParticipantesSerializado { get; set; }
        public List<string> Participantes
        {
            get
            {
                if (ParticipantesSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(ParticipantesSerializado);
            }
        }


        public string PublicoAlvoSerializado { get; set; }
        public List<string> PublicoAlvo
        {
            get
            {
                if (PublicoAlvoSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(PublicoAlvoSerializado);
            }
        }


        public string MeiosDeDivulgacaoSerializado { get; set; }
        public List<string> MeiosDeDivulgacao
        {
            get
            {
                if (MeiosDeDivulgacaoSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(MeiosDeDivulgacaoSerializado);
            }
        }

        public string ParceriasSerializado { get; set; }
        public List<string> Parcerias
        {
            get
            {
                if (ParceriasSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(ParceriasSerializado);
            }
        }

        public string TarefasSerializado { get; set; }
        public List<Tarefa> Tarefas
        {
            get
            {
                if (TarefasSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<Tarefa>>(TarefasSerializado);
            }
        }

        public string LancamentosFinanceirosSerializado { get; set; }
        public List<LancamentoFinanceiro> LancamentosFinanceiros
        {
            get
            {
                if (LancamentosFinanceirosSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<LancamentoFinanceiro>>(LancamentosFinanceirosSerializado);
            }
        }

        #endregion

        public Projeto()
        {

        }

        public Projeto(CadastrarProjetoInput input)
        {
            Codigo = input.Codigo;
            CodigoClube = input.CodigoClube;
            DataFim = input.DataFim;
            DataFinalizacao = input.DataFinalizacao;
            DataInicio = input.DataInicio;
            DataUltimaAtualizacao = input.DataUltimaAtualizacao;
            Descricao = input.Descricao;
            Dificuldade = input.Dificuldade;
            Fotos = input.Fotos;
            Justificativa = input.Justificativa;
            LicoesAprendidas = input.LicoesAprendidas;
            Nome = input.Nome;
            NomeClube = input.NomeClube;
            NumeroDistrito = input.NumeroDistrito;
            PalavraChave = input.PalavraChave;
            Resultados = input.Resultados;
            Resumo = input.Resumo;
            MeiosDeDivulgacaoSerializado = JsonConvert.SerializeObject(input.MeiosDeDivulgacao);
            ObjetivosEspecificosSerializado = JsonConvert.SerializeObject(input.ObjetivosEspecificos);
            ObjetivosGeraisSerializado = JsonConvert.SerializeObject(input.ObjetivosGerais);
            ParceriasSerializado = JsonConvert.SerializeObject(input.Parcerias);
            ParticipantesSerializado = JsonConvert.SerializeObject(input.Participantes);
            PublicoAlvoSerializado = JsonConvert.SerializeObject(input.PublicoAlvo);
            CategoriasPrincipaisSerializado = JsonConvert.SerializeObject(input.CategoriasPrincipais);
            CategoriasSecundariasSerializado = JsonConvert.SerializeObject(input.CategoriasSecundarias);
            LancamentosFinanceirosSerializado = JsonConvert.SerializeObject(input.LancamentosFinanceiros
                .Select(x => new LancamentoFinanceiro(x.Data, x.Descricao, x.Valor)).ToList());
            TarefasSerializado = JsonConvert.SerializeObject(input.Tarefas
                .Select(x => new Tarefa(x.Data, x.Descricao)).ToList());

            DataAtualizacao = DateTime.Now;

            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Codigo.ToString();
        }

        public void Atualizar(CadastrarProjetoInput input)
        {
            CodigoClube = input.CodigoClube;
            DataFim = input.DataFim;
            DataFinalizacao = input.DataFinalizacao;
            DataInicio = input.DataInicio;
            DataUltimaAtualizacao = input.DataUltimaAtualizacao;
            Descricao = input.Descricao;
            Dificuldade = input.Dificuldade;
            Fotos = input.Fotos;
            Justificativa = input.Justificativa;
            LicoesAprendidas = input.LicoesAprendidas;
            Nome = input.Nome;
            NomeClube = input.NomeClube;
            NumeroDistrito = input.NumeroDistrito;
            PalavraChave = input.PalavraChave;
            Resultados = input.Resultados;
            Resumo = input.Resumo;
            MeiosDeDivulgacaoSerializado = JsonConvert.SerializeObject(input.MeiosDeDivulgacao);
            ObjetivosEspecificosSerializado = JsonConvert.SerializeObject(input.ObjetivosEspecificos);
            ObjetivosGeraisSerializado = JsonConvert.SerializeObject(input.ObjetivosGerais);
            ParceriasSerializado = JsonConvert.SerializeObject(input.Parcerias);
            ParticipantesSerializado = JsonConvert.SerializeObject(input.Participantes);
            PublicoAlvoSerializado = JsonConvert.SerializeObject(input.PublicoAlvo);
            CategoriasPrincipaisSerializado = JsonConvert.SerializeObject(input.CategoriasPrincipais);
            CategoriasSecundariasSerializado = JsonConvert.SerializeObject(input.CategoriasSecundarias);
            LancamentosFinanceirosSerializado = JsonConvert.SerializeObject(input.LancamentosFinanceiros
                .Select(x => new LancamentoFinanceiro(x.Data, x.Descricao, x.Valor)).ToList());
            TarefasSerializado = JsonConvert.SerializeObject(input.Tarefas
                .Select(x => new Tarefa(x.Data, x.Descricao)).ToList());

            DataAtualizacao = DateTime.Now;
        }

        public void Atualizar(Projeto input)
        {
            CodigoClube = input.CodigoClube;
            DataFim = input.DataFim;
            DataFinalizacao = input.DataFinalizacao;
            DataInicio = input.DataInicio;
            DataUltimaAtualizacao = input.DataUltimaAtualizacao;
            Descricao = input.Descricao;
            Dificuldade = input.Dificuldade;
            Fotos = input.Fotos;
            Justificativa = input.Justificativa;
            LicoesAprendidas = input.LicoesAprendidas;
            Nome = input.Nome;
            NomeClube = input.NomeClube;
            NumeroDistrito = input.NumeroDistrito;
            PalavraChave = input.PalavraChave;
            Resultados = input.Resultados;
            Resumo = input.Resumo;
            MeiosDeDivulgacaoSerializado = JsonConvert.SerializeObject(input.MeiosDeDivulgacao);
            ObjetivosEspecificosSerializado = JsonConvert.SerializeObject(input.ObjetivosEspecificos);
            ObjetivosGeraisSerializado = JsonConvert.SerializeObject(input.ObjetivosGerais);
            ParceriasSerializado = JsonConvert.SerializeObject(input.Parcerias);
            ParticipantesSerializado = JsonConvert.SerializeObject(input.Participantes);
            PublicoAlvoSerializado = JsonConvert.SerializeObject(input.PublicoAlvo);
            CategoriasPrincipaisSerializado = JsonConvert.SerializeObject(input.CategoriasPrincipais);
            CategoriasSecundariasSerializado = JsonConvert.SerializeObject(input.CategoriasSecundarias);
            LancamentosFinanceirosSerializado = JsonConvert.SerializeObject(input.LancamentosFinanceiros
                .Select(x => new LancamentoFinanceiro(x.Data, x.Descricao, x.Valor)).ToList());
            TarefasSerializado = JsonConvert.SerializeObject(input.Tarefas
                .Select(x => new Tarefa(x.Data, x.Descricao)).ToList());

            DataAtualizacao = DateTime.Now;
        }
    }
}
