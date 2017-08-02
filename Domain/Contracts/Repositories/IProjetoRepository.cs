using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IProjetoRepository
    {
        Projeto Buscar(int codigo);
        Projeto Incluir(Projeto projeto);
        void Atualizar(Projeto projeto);
    }
}
