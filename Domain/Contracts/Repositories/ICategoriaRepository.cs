using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface ICategoriaRepository
    {
        Categoria Buscar(string nome);
        Categoria Incluir(Categoria categoria);
    }
}
