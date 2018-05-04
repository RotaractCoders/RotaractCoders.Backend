using Domain.Entities.Base;
using System;

namespace Domain.Entities
{
    public class LancamentoFinanceiro : Entity
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        protected LancamentoFinanceiro()
        {

        }

        public LancamentoFinanceiro(DateTime data, string descricao, decimal valor)
        {
            Data = data;
            Descricao = descricao;
            Valor = valor;
        }
    }
}
