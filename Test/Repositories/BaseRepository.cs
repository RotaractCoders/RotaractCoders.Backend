using Domain.Contracts.Repositories;
using Infra;
using Infra.Repositories;

namespace Test.Repositories
{
    public class BaseRepository
    {
        public IDistritoRepository DistritoRepository;

        public BaseRepository()
        {
            var context = new Context(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ControleCasaDBTest;Integrated Security=True");
            DistritoRepository = new DistritoRepository(context);
        }
    }
}
