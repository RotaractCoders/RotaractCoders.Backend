using Domain.Commands.Inputs;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Socio : TableEntity
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Celular { get; set; }
        public string Clube { get; set; }
        public string Foto { get; set; }
        public string CargosSerializado { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool BitAtivo { get; set; } = true;

        public List<Cargo> Cargos
        {
            get
            {
                if (CargosSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<Cargo>>(CargosSerializado);
            }
        }

        public Socio()
        {

        }

        public Socio(CadastroSocioInput input)
        {
            Nome = input.Nome;
            Apelido = input.Apelido;
            DataNascimento = input.DataNascimento;
            Email = input.Email;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Celular = input.Celular;
            Clube = input.Clube;
            Foto = input.Foto;
            DataAtualizacao = DateTime.Now;

            if (input.Cargos != null)
            {
                var cargos = input.Cargos.Select(x => new Cargo(x.Nome, x.TipoCargo, x.GestaoDe, x.GestaoAte));
                CargosSerializado = JsonConvert.SerializeObject(cargos);
            }
            else
            {
                CargosSerializado = null;
            }

            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Nome;
        }

        public void Atualizar(Socio input)
        {
            Nome = input.Nome;
            Apelido = input.Apelido;
            DataNascimento = input.DataNascimento;
            Email = input.Email;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Celular = input.Celular;
            Clube = input.Clube;
            Foto = input.Foto;
            DataAtualizacao = DateTime.Now;

            if (input.Cargos != null)
            {
                var cargos = input.Cargos.Select(x => new Cargo(x.Nome, x.TipoCargo, x.GestaoDe, x.GestaoAte));
                CargosSerializado = JsonConvert.SerializeObject(cargos);
            }
            else
            {
                CargosSerializado = null;
            }

            PartitionKey = Nome;
        }

        public void Atualizar(CadastroSocioInput input)
        {
            Nome = input.Nome;
            Apelido = input.Apelido;
            DataNascimento = input.DataNascimento;
            Email = input.Email;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Celular = input.Celular;
            Clube = input.Clube;
            Foto = input.Foto;
            DataAtualizacao = DateTime.Now;

            if (input.Cargos != null)
            {
                var cargos = input.Cargos.Select(x => new Cargo(x.Nome, x.TipoCargo, x.GestaoDe, x.GestaoAte));
                CargosSerializado = JsonConvert.SerializeObject(cargos);
            }
            else
            {
                CargosSerializado = null;
            }

            PartitionKey = Nome;
        }

        public void Inativar()
        {
            BitAtivo = true;
            DataAtualizacao = DateTime.Now;
        }
    }
}
