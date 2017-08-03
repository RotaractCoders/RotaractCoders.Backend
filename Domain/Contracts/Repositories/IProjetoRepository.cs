using Domain.Commands.Inputs;
using Domain.Commands.Results;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories
{
    public interface IProjetoRepository
    {
        Projeto Buscar(int codigo);
        ConsultaProjetoResult Obter(Guid idProjeto);
        List<ListaProjetosResult> Listar(ListaProjetosInput input);
        Projeto Incluir(Projeto projeto);
        void Atualizar(Projeto projeto);
    }
}
