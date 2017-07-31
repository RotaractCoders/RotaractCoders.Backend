using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface ISocioRepository
    {
        Socio Buscar(int codigo);
        void Incluir(Socio socio);
        void Atualizar(Socio socio);
    }
}
