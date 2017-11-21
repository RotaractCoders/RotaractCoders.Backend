using Domain.Commands.Results;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories
{
    public interface IEventoRepository
    {
        List<ListaEventoResult> Listar();
        DetalheEventoResult Buscar(Guid id);
        Evento Obter(Guid id);
        void Incluir(Evento evento);
        void Atualizar(Evento evento);
        void Deletar(Guid id);
    }
}
