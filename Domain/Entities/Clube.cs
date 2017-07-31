using Domain.Commands.Inputs;
using Domain.Entities.Base;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Clube : Entity
    {
        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Site { get; private set; }
        public string Facebook { get; private set; }
        public string Email { get; private set; }
        public DateTime? DataFundacao { get; private set; }
        public string RotaryPadrinho { get; private set; }
        public DateTime? DataFechamento { get; private set; }
        public Guid IdDistrito { get; private set; }
        public Distrito Distrito { get; private set; }
        public List<Projeto> Projetos { get; private set; }
        public List<SocioClube> SociosClube { get; private set; }
        public List<CargoClube> CargosClube { get; private set; }

        protected Clube()
        {

        }

        public Clube(CriarClubeInput input, Guid idDistrito)
        {
            AdicionarValidacoes(input);

            if (!IsValid())
                return;

            Codigo = input.Codigo;
            Nome = input.Nome;
            Site = input.Site;
            Facebook = input.Facebook;
            Email = input.Email;
            DataFundacao = input.DataFundacao;
            DataFechamento = input.DataFechamento;
            RotaryPadrinho = input.RotaryPadrinho;
            IdDistrito = idDistrito;
        }

        public Clube Atualizar(CriarClubeInput input, Guid idDistrito)
        {
            AdicionarValidacoes(input);

            if (!IsValid())
                return null;

            Nome = input.Nome;
            Site = input.Site;
            Facebook = input.Facebook;
            Email = input.Email;
            DataFundacao = input.DataFundacao;
            DataFechamento = input.DataFechamento;
            RotaryPadrinho = input.RotaryPadrinho;
            IdDistrito = idDistrito;

            return this;
        }

        private void AdicionarValidacoes(CriarClubeInput input)
        {
            new ValidationContract<CriarClubeInput>(input)
                .IsGreaterThan(x => x.Codigo, 0, "O código é obrigatório")
                .IsRequired(x => x.Nome, "O nome é obrigatório")
                .IsRequired(x => x.numeroDistrito, "O número do distrito é obrigatório");
        }
    }
}
