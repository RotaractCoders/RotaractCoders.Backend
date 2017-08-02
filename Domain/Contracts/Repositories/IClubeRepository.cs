using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories
{
    public interface IClubeRepository
    {
        Clube Buscar(int codigo);

        Clube BuscarPorNome(string nomeClube);

        void Incluir(Clube clube);

        void Atualizar(Clube clube);
    }
}
