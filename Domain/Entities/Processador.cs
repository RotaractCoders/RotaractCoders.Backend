using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Domain.Entities
{
    public class Processador : TableEntity
    {
        public string NumeroDistrito { get; set; }

        public int QuantidadeClubes { get; set; }
        public int QuantidadeSocios { get; set; }
        public int QuantidadeProjetos { get; set; }

        public string StatusProcessamentoClube { get; set; }
        public string StatusProcessamentoSocio { get; set; }
        public string StatusProcessamentoProjeto { get; set; }

        public string DescricaoProcessamentoClube { get; set; }
        public string DescricaoProcessamentoSocio { get; set; }
        public string DescricaoProcessamentoProjeto { get; set; }

        public DateTime? InicioProcessamentoClube { get; set; }
        public DateTime? FimProcessamentoClube { get; set; }

        public DateTime? InicioProcessamentoSocio { get; set; }
        public DateTime? FimProcessamentoSocio { get; set; }

        public DateTime? InicioProcessamentoProjeto { get; set; }
        public DateTime? FimProcessamentoProjeto { get; set; }

        public Processador()
        {

        }

        public Processador(string numeroDistrito, int quantidadeClubes, int quantidadeSocios, int quantidadeProjetos)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Guid.NewGuid().ToString();

            QuantidadeClubes = quantidadeClubes;
            QuantidadeProjetos = quantidadeProjetos;
            QuantidadeSocios = quantidadeSocios;

            StatusProcessamentoClube = "AguardandoProcessamento";
            StatusProcessamentoSocio = "AguardandoProcessamento";
            StatusProcessamentoProjeto = "AguardandoProcessamento";

            DescricaoProcessamentoClube = string.Empty;
            DescricaoProcessamentoSocio = string.Empty;
            DescricaoProcessamentoProjeto = string.Empty;

            NumeroDistrito = numeroDistrito;
        }

        public void IniciarProcessamentoClube()
        {
            StatusProcessamentoClube = "Processando";
            InicioProcessamentoClube = DateTime.Now;
        }

        public void FinalizarProcessamentoClube(string erro = "")
        {
            FimProcessamentoClube = DateTime.Now;

            if (erro == "")
            {
                StatusProcessamentoClube = "Finalizado";
                return;
            }

            StatusProcessamentoClube = "ComErro";
            DescricaoProcessamentoClube = erro;
        }

        public void IniciarProcessamentoSocio()
        {
            StatusProcessamentoSocio = "Processando";
            InicioProcessamentoSocio = DateTime.Now;
        }

        public void FinalizarProcessamentoSocio(string erro = "")
        {
            FimProcessamentoSocio = DateTime.Now;

            if (erro == "")
            {
                StatusProcessamentoSocio = "Finalizado";
                return;
            }

            StatusProcessamentoSocio = "ComErro";
            DescricaoProcessamentoSocio = erro;
        }

        public void IniciarProcessamentoProjeto()
        {
            StatusProcessamentoProjeto = "Processando";
            InicioProcessamentoProjeto = DateTime.Now;
        }

        public void FinalizarProcessamentoProjeto(string erro = "")
        {
            FimProcessamentoProjeto = DateTime.Now;

            if (erro == "")
            {
                StatusProcessamentoProjeto = "Finalizado";
                return;
            }

            StatusProcessamentoProjeto = "ComErro";
            DescricaoProcessamentoProjeto = erro;
        }

        public Processador Atualizar(Processador processador)
        {
            NumeroDistrito = processador.NumeroDistrito;
            QuantidadeClubes = processador.QuantidadeClubes;
            QuantidadeSocios = processador.QuantidadeSocios;
            QuantidadeProjetos = processador.QuantidadeProjetos;
            StatusProcessamentoClube = processador.StatusProcessamentoClube;
            StatusProcessamentoSocio = processador.StatusProcessamentoSocio;
            StatusProcessamentoProjeto = processador.StatusProcessamentoProjeto;
            DescricaoProcessamentoClube = processador.DescricaoProcessamentoClube;
            DescricaoProcessamentoSocio = processador.DescricaoProcessamentoSocio;
            DescricaoProcessamentoProjeto = processador.DescricaoProcessamentoProjeto;
            InicioProcessamentoClube = processador.InicioProcessamentoClube;
            FimProcessamentoClube = processador.FimProcessamentoClube;
            InicioProcessamentoSocio = processador.InicioProcessamentoSocio;
            FimProcessamentoSocio = processador.FimProcessamentoSocio;
            InicioProcessamentoProjeto = processador.InicioProcessamentoProjeto;
            FimProcessamentoProjeto = processador.FimProcessamentoProjeto;

            return processador;
        }
    }
}
