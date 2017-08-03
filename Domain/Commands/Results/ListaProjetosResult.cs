using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Commands.Results
{
    public class ListaProjetosResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public List<string> Categorias { get; set; }
        public string Clube { get; set; }
        public string Distrito { get; set; }

        public ListaProjetosResult(Projeto projeto)
        {
            Id = projeto.Id;
            Nome = projeto.Nome;
            Resumo = projeto.Resumo;
            Clube = projeto.Clube.Nome;
            Distrito = projeto.Clube.Distrito.Numero;
            Categorias = projeto.ProjetoCategorias
                .Where(x => x.TipoCategoria == TipoCategoria.Principal)
                .Select(x => x.Categoria.Nome).ToList();
        }
    }
}
