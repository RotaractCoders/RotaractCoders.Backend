using Domain.Entities;
using Domain.Enum;

namespace Domain.Contracts.Repositories
{
    public interface ICargoRepository
    {
        Cargo Buscar(string nome, TipoCargo tipoCargo);
        Cargo Incluir(Cargo cargo);
    }
}
