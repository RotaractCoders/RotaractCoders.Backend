using Domain.Commands.Results;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories
{
    public interface IEventoRepository
    {
        List<Evento> Listar();
        DetalheEventoResult Buscar(string id);
        Evento Obter(string id);
        void Incluir(Evento evento);
        void Atualizar(Evento evento);
        void Deletar(string id);
    }
}
