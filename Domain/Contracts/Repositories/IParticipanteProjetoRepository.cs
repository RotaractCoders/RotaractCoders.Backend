using Domain.Entities;
using System;

namespace Domain.Contracts.Repositories
{
    public interface IParticipanteProjetoRepository
    {
        void ExcluirPorProjeto(Guid idProjeto);
        ParticipanteProjeto Incluir(ParticipanteProjeto participanteProjeto);
    }
}
