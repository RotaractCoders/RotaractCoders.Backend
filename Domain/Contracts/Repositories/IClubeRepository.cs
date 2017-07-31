using Domain.Entities;
using System;

namespace Domain.Contracts.Repositories
{
    public interface IClubeRepository
    {
        Clube Buscar(int codigo);

        Clube BuscarPorNomeEDistrito(string nomeClube);

        void Incluir(Clube clube);

        void Atualizar(Clube clube);
    }
}
