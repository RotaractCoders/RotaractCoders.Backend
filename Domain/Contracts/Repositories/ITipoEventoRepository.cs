using Domain.Commands.Results;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface ITipoEventoRepository
    {
        List<ListaTipoEventoResult> Listar();
        void Incluir(TipoEvento tipoEvento);
        void Deletar(Guid id);
    }
}
