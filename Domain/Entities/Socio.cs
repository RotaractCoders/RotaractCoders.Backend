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
        public string Nome { get; private set; }
        public string Apelido { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public string Email { get; private set; }
        public string Facebook { get; private set; }
        public string Instagram { get; private set; }
        public string Celular { get; private set; }
        public string CargosSerializado { get; private set; }
        public DateTime DataAtualizacao { get; private set; }

        public List<Cargo> Cargos => JsonConvert.DeserializeObject<List<Cargo>>(CargosSerializado);

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
            DataAtualizacao = DateTime.Now;

            var cargos = input.Cargos.Select(x => new Cargo(x.Nome, x.TipoCargo, x.GestaoDe, x.GestaoAte));
            CargosSerializado = JsonConvert.SerializeObject(cargos);

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
            DataAtualizacao = DateTime.Now;

            var cargos = input.Cargos.Select(x => new Cargo(x.Nome, x.TipoCargo, x.GestaoDe, x.GestaoAte));
            CargosSerializado = JsonConvert.SerializeObject(cargos);

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
            DataAtualizacao = DateTime.Now;

            var cargos = input.Cargos.Select(x => new Cargo(x.Nome, x.TipoCargo, x.GestaoDe, x.GestaoAte));
            CargosSerializado = JsonConvert.SerializeObject(cargos);

            PartitionKey = Nome;
        }
    }
}
