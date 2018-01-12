using Domain.Commands.Inputs;
using Domain.Contracts.Commands;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Enum;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Commands.Handlers
{
    public class CadastrarProjetoHandler : Notifiable,
        ICommandHandler<CadastrarProjetoInput>
    {
        private IProjetoRepository _projetoRepository;
        private IClubeRepository _clubeRepository;
        private IDistritoRepository _distritoRepository;
        private ILancamentoFinanceiroRepository _lancamentoFinanceiroRepository;
        private IParticipanteProjetoRepository _participanteProjetoRepository;
        private IPublicoAlvoProjetoRepository _publicoAlvoProjetoRepository;
        private IMeioDeDivulgacaoProjetoRepository _meioDeDivulgacaoProjetoRepository;
        private IParceriaProjetoRepository _parceriaProjetoRepository;
        private ITarefaRepository _tarefaRepository;
        private IObjetivoRepository _objetivoRepository;
        private IProjetoCategoriaRepository _projetoCategoriaRepository;
        private ICategoriaRepository _categoriaRepository;

        public CadastrarProjetoHandler(
            IProjetoRepository projetoRepository,
            IClubeRepository clubeRepository,
            IDistritoRepository distritoRepository,
            ILancamentoFinanceiroRepository lancamentoFinanceiroRepository,
            IParticipanteProjetoRepository participanteProjetoRepository,
            IPublicoAlvoProjetoRepository publicoAlvoProjetoRepository,
            IMeioDeDivulgacaoProjetoRepository meioDeDivulgacaoProjetoRepository,
            IParceriaProjetoRepository parceriaProjetoRepository,
            ITarefaRepository tarefaRepository,
            IObjetivoRepository objetivoRepository,
            IProjetoCategoriaRepository projetoCategoriaRepository,
            ICategoriaRepository categoriaRepository)
        {
            _projetoRepository = projetoRepository;
            _clubeRepository = clubeRepository;
            _distritoRepository = distritoRepository;
            _lancamentoFinanceiroRepository = lancamentoFinanceiroRepository;
            _participanteProjetoRepository = participanteProjetoRepository;
            _publicoAlvoProjetoRepository = publicoAlvoProjetoRepository;
            _meioDeDivulgacaoProjetoRepository = meioDeDivulgacaoProjetoRepository;
            _parceriaProjetoRepository = parceriaProjetoRepository;
            _tarefaRepository = tarefaRepository;
            _objetivoRepository = objetivoRepository;
            _projetoCategoriaRepository = projetoCategoriaRepository;
            _categoriaRepository = categoriaRepository;
        }

        public ICommandResult Handle(CadastrarProjetoInput command)
        {
            var clube = CadastrarClube(command);
            var projeto = CadastrarProjeto(command, clube);

            CadastrarLancamentosFinanceiros(command, projeto);
            CadastrarParticipantes(command, projeto);
            CadastrarPublicoAlvo(command, projeto);
            CadastrarMeiosDeDivulgacao(command, projeto);
            CadastrarParcerias(command, projeto);
            CadastrarTarefas(command, projeto);
            CadastrarObjetivos(command, projeto);
            CadastrarProjetoCategorias(command, projeto);
            CadastrarPublicoAlvo(command, projeto);
            CadastrarMeiosDeDivulgacao(command, projeto);
            CadastrarParcerias(command, projeto);
            CadastrarTarefas(command, projeto);
            CadastrarObjetivos(command, projeto);

            return null;
        }

        private Clube CadastrarClube(CadastrarProjetoInput command)
        {
            var clube = _clubeRepository.Buscar(command.CodigoClube);

            if (clube == null)
            {
                var distrito = _distritoRepository.Buscar(command.NumeroDistrito);

                if (distrito == null)
                {
                    distrito = new Distrito(new CriarDistritoInput
                    {
                        Numero = command.NumeroDistrito
                    });
                    distrito = _distritoRepository.Incluir(distrito);
                }

                //clube = new Clube(new CriarClubeInput
                //{
                //    Codigo = command.CodigoClube,
                //    Nome = command.NomeClube
                //}, distrito.Id);

                _clubeRepository.Incluir(clube);
            }

            return clube;
        }

        private Projeto CadastrarProjeto(CadastrarProjetoInput command, Clube clube)
        {
            var projeto = _projetoRepository.Buscar(command.Codigo);

            if (projeto == null)
            {
                //projeto = new Projeto(command, clube.Id);
                projeto = _projetoRepository.Incluir(projeto);
            }
            else
            {
                projeto.Atualizar(command);
                _projetoRepository.Atualizar(projeto);
            }

            return projeto;
        }

        private List<LancamentoFinanceiro> CadastrarLancamentosFinanceiros(CadastrarProjetoInput command, Projeto projeto)
        {
            _lancamentoFinanceiroRepository.ExcluirPorProjeto(projeto.Id);

            var lista = new List<LancamentoFinanceiro>();

            command.LancamentosFinanceiros.ForEach(x =>
            {
                var lancamento = new LancamentoFinanceiro(x.Data, x.Descricao, x.Valor, projeto.Id);
                lancamento = _lancamentoFinanceiroRepository.Incluir(lancamento);

                lista.Add(lancamento);
            });

            return lista;
        }

        private List<ParticipanteProjeto> CadastrarParticipantes(CadastrarProjetoInput command, Projeto projeto)
        {
            _participanteProjetoRepository.ExcluirPorProjeto(projeto.Id);

            var lista = new List<ParticipanteProjeto>();

            command.Participantes.ForEach(x =>
            {
                var participante = new ParticipanteProjeto(x, projeto.Id);
                participante = _participanteProjetoRepository.Incluir(participante);

                lista.Add(participante);
            });

            return lista;
        }

        private List<PublicoAlvoProjeto> CadastrarPublicoAlvo(CadastrarProjetoInput command, Projeto projeto)
        {
            _publicoAlvoProjetoRepository.ExcluirPorProjeto(projeto.Id);

            var lista = new List<PublicoAlvoProjeto>();

            command.PublicoAlvo.ForEach(x =>
            {
                var publicoAlvoProjeto = new PublicoAlvoProjeto(x, projeto.Id);
                publicoAlvoProjeto = _publicoAlvoProjetoRepository.Incluir(publicoAlvoProjeto);

                lista.Add(publicoAlvoProjeto);
            });

            return lista;
        }

        private List<MeioDeDivulgacaoProjeto> CadastrarMeiosDeDivulgacao(CadastrarProjetoInput command, Projeto projeto)
        {
            _meioDeDivulgacaoProjetoRepository.ExcluirPorProjeto(projeto.Id);

            var lista = new List<MeioDeDivulgacaoProjeto>();

            command.MeiosDeDivulgacao.ForEach(x =>
            {
                var meioDeDivulgacaoProjeto = new MeioDeDivulgacaoProjeto(x, projeto.Id);
                meioDeDivulgacaoProjeto = _meioDeDivulgacaoProjetoRepository.Incluir(meioDeDivulgacaoProjeto);

                lista.Add(meioDeDivulgacaoProjeto);
            });

            return lista;
        }

        private List<ParceriaProjeto> CadastrarParcerias(CadastrarProjetoInput command, Projeto projeto)
        {
            _parceriaProjetoRepository.ExcluirPorProjeto(projeto.Id);

            var lista = new List<ParceriaProjeto>();

            command.Parcerias.ForEach(x =>
            {
                var parceriaProjeto = new ParceriaProjeto(x, projeto.Id);
                parceriaProjeto = _parceriaProjetoRepository.Incluir(parceriaProjeto);

                lista.Add(parceriaProjeto);
            });

            return lista;
        }

        private List<Tarefa> CadastrarTarefas(CadastrarProjetoInput command, Projeto projeto)
        {
            _tarefaRepository.ExcluirPorProjeto(projeto.Id);

            var lista = new List<Tarefa>();

            command.Tarefas.ForEach(x =>
            {
                var tarefa = new Tarefa(x.Data, x.Descricao, projeto.Id);
                tarefa = _tarefaRepository.Incluir(tarefa);

                lista.Add(tarefa);
            });

            return lista;
        }

        private List<Objetivo> CadastrarObjetivos(CadastrarProjetoInput command, Projeto projeto)
        {
            _objetivoRepository.ExcluirPorProjeto(projeto.Id);

            var lista = new List<Objetivo>();

            command.ObjetivosEspecificos.ForEach(x =>
            {
                var objetivo = new Objetivo(x, projeto.Id, TipoObjetivo.Especifico);
                objetivo = _objetivoRepository.Incluir(objetivo);

                lista.Add(objetivo);
            });

            command.ObjetivosGerais.ForEach(x =>
            {
                var objetivo = new Objetivo(x, projeto.Id, TipoObjetivo.Geral);
                objetivo = _objetivoRepository.Incluir(objetivo);

                lista.Add(objetivo);
            });

            return lista;
        }

        private List<ProjetoCategoria> CadastrarProjetoCategorias(CadastrarProjetoInput command, Projeto projeto)
        {
            _projetoCategoriaRepository.ExcluirPorProjeto(projeto.Id);

            var lista = new List<ProjetoCategoria>();

            command.CategoriasPrincipais.ForEach(x =>
            {
                var categoria = _categoriaRepository.Buscar(x);
                if (categoria == null)
                    categoria = _categoriaRepository.Incluir(new Categoria(x));

                var projetoCategoria = new ProjetoCategoria(projeto.Id, categoria.Id, TipoCategoria.Principal);
                projetoCategoria = _projetoCategoriaRepository.Incluir(projetoCategoria);

                lista.Add(projetoCategoria);
            });

            command.CategoriasSecundarias.ForEach(x =>
            {
                var categoria = _categoriaRepository.Buscar(x);
                if (categoria == null)
                    categoria = _categoriaRepository.Incluir(new Categoria(x));

                var projetoCategoria = new ProjetoCategoria(projeto.Id, categoria.Id, TipoCategoria.Secundaria);
                projetoCategoria = _projetoCategoriaRepository.Incluir(projetoCategoria);

                lista.Add(projetoCategoria);
            });

            return lista;
        }
    }
}
