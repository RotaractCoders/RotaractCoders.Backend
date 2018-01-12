using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Data.Entity;
using Domain.Commands.Inputs;
using Domain.Commands.Results;
using System.Linq.Expressions;
using Domain.Enum;

namespace Infra.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        private Context _context;

        public ProjetoRepository()
        {
            _context = new Context();
        }

        public void Atualizar(Projeto projeto)
        {
            _context.Entry(projeto).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Projeto Buscar(int codigo)
        {
            return _context.Projeto.AsNoTracking().FirstOrDefault(x => x.Codigo == codigo);
        }

        public Projeto Incluir(Projeto projeto)
        {
            projeto = _context.Projeto.Add(projeto);
            _context.SaveChanges();

            return projeto;
        }

        public List<ListaProjetosResult> Listar(ListaProjetosInput input)
        {
            Func<Projeto, bool> filtroCategoria = null;
            Func<Projeto, bool> filtroDistrito = null;
            Func<Projeto, bool> filtroClube = null;

            if (input.IdCategoria != Guid.Empty)
                filtroCategoria = new Func<Projeto, bool>(x => x.ProjetoCategorias.Any(a => a.TipoCategoria == TipoCategoria.Principal && a.IdCategoria == input.IdCategoria));

            //if (input.IdDistrito != Guid.Empty)
            //    filtroDistrito = new Func<Projeto, bool>(x => x.Clube.IdDistrito == input.IdDistrito);

            if (input.IdClube != Guid.Empty)
                filtroClube = new Func<Projeto, bool>(x => x.IdClube == input.IdClube);

            var consulta = filtroDistrito + filtroClube + filtroCategoria;
            if (consulta == null)
                consulta = new Func<Projeto, bool>(x => true);

            //return _context.Projeto
            //    .Include(x => x.ProjetoCategorias)
            //    .Include("ProjetoCategorias.Categoria")
            //    .Include(x => x.Clube)
            //    .Include(x => x.Clube.Distrito)
            //    .AsNoTracking()
            //    .Where(consulta)
            //    .Take(10)
            //    .ToList()
            //    .Select(x => new ListaProjetosResult(x))
            //    .ToList();

            return null;
        }

        public ConsultaProjetoResult Obter(Guid idProjeto)
        {
            //var projeto = _context.Projeto
            //    .Include(x => x.LancamentosFinanceiros)
            //    .Include(x => x.MeiosDeDivulgacao)
            //    .Include(x => x.Objetivos)
            //    .Include(x => x.Parcerias)
            //    .Include(x => x.Participantes)
            //    .Include(x => x.ProjetoCategorias)
            //    .Include("ProjetoCategorias.Categoria")
            //    .Include(x => x.PublicoAlvo)
            //    .Include(x => x.Tarefas)
            //    .Include(x => x.Clube)
            //    .Include(x => x.Clube.Distrito)
            //    .AsNoTracking()
            //    .FirstOrDefault(x => x.Id == idProjeto);

            //if (projeto == null)
            //    return null;

            //return new ConsultaProjetoResult(projeto);

            return null;
        }
    }
}
