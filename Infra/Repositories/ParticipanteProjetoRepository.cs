using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Repositories
{
    public class ParticipanteProjetoRepository : IParticipanteProjetoRepository
    {
        private Context _context;

        public ParticipanteProjetoRepository()
        {
            _context = new Context();
        }

        public void ExcluirPorProjeto(Guid idProjeto)
        {
            _context.ParticipanteProjeto
                .Where(x => x.IdProjeto == idProjeto)
                .ToList()
                .ForEach(x => _context.ParticipanteProjeto.Remove(x));

            _context.SaveChanges();
        }

        public ParticipanteProjeto Incluir(ParticipanteProjeto participanteProjeto)
        {
            participanteProjeto = _context.ParticipanteProjeto.Add(participanteProjeto);
            _context.SaveChanges();

            return participanteProjeto;
        }
    }
}
