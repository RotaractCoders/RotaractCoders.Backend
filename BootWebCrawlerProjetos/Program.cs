using Domain.Commands.Handlers;
using Infra.Repositories;
using Infra.WebCrowley;
using System;

namespace BootWebCrawlerProjetos
{
    class Program
    {
        static void Main(string[] args)
        {
            var cadastroProjetoHandler = new CadastrarProjetoHandler(
                new ProjetoRepository(),
                new ClubeRepository(),
                new DistritoRepository(),
                new LancamentoFinanceiroRepository(),
                new ParticipanteProjetoRepository(),
                new PublicoAlvoProjetoRepository(),
                new MeioDeDivulgacaoProjetoRepository(),
                new ParceriaProjetoRepository(),
                new TarefaRepository(),
                new ObjetivoRepository(),
                new ProjetoCategoriaRepository(),
                new CategoriaRepository());

            var omirBrasilRepository = new OmirBrasilRepository();

            for (int i = 0; i < 15000; i++)
            {
                try
                {
                    var projeto = omirBrasilRepository.BuscarProjetoPorCodigo(i);

                    if (projeto == null)
                    {
                        Console.WriteLine($"Projeto {i} - Não encontrado");
                    }
                    else
                    {
                        //cadastroProjetoHandler.Handle(projeto);

                        Console.WriteLine($"Projeto {i} - Cadastrado");
                    }
                }
                catch
                {
                    Console.WriteLine($"Projeto {i} - Erro");

                    while(true)
                    {
                        Console.Beep();
                    }

                    Console.ReadKey();
                }
            }
        }
    }
}
