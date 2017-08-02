using Infra.WebCrowley;
using System;

namespace BootWebCrawlerProjetos
{
    class Program
    {
        static void Main(string[] args)
        {
            var omirBrasilRepository = new OmirBrasilRepository();

            for (int i = 0; i < 15000; i++)
            {
                var projeto = omirBrasilRepository.GetByCode(i);

                if (projeto == null)
                {
                    Console.WriteLine($"Projeto {i} - Não encontrado");
                }
                else
                {
                    Console.WriteLine($"Projeto {i} - Cadastrado");
                }
            }

            Console.ReadKey();
        }
    }
}
