using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IDistritoRepository
    {
        Distrito Buscar(string numero);

        Distrito Incluir(Distrito distrito);

        Distrito Atualizar(Distrito distrito);
    }
}
