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
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Celular { get; set; }
        
        public string Foto { get; set; }
        public string CargosSerializado { get; set; }
        public string ClubesSerializado { get; set; }
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

        public List<SocioClube> Clubes
        {
            get
            {
                if (ClubesSerializado == null)
                    return null;

                return JsonConvert.DeserializeObject<List<SocioClube>>(ClubesSerializado);
            }
        }

        public Socio()
        {

        }

        public Socio(CadastroSocioInput input)
        {
            Codigo = input.Codigo;
            Nome = input.Nome;
            Apelido = input.Apelido;
            DataNascimento = input.DataNascimento;
            Email = input.Email;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Celular = input.Celular;
            Foto = input.Foto;
            DataAtualizacao = DateTime.Now;

            if (input.Cargos != null)
            {
                var cargos = input.Cargos.Select(x => new Cargo(x.Nome, x.TipoCargo, x.De, x.Ate));
                CargosSerializado = JsonConvert.SerializeObject(cargos);
            }
            else
            {
                CargosSerializado = null;
            }

            if (input.Clubes != null)
            {
                var clubes = input.Clubes.Select(x => new SocioClube(x.NumeroDistrito, x.NomeClube, x.Posse, x.Desligamento));
                CargosSerializado = JsonConvert.SerializeObject(clubes);
            }
            else
            {
                CargosSerializado = null;
            }

            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Codigo;
        }

        public void Atualizar(Socio input)
        {
            Codigo = input.Codigo;
            Nome = input.Nome;
            Apelido = input.Apelido;
            DataNascimento = input.DataNascimento;
            Email = input.Email;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Celular = input.Celular;
            Foto = input.Foto;
            DataAtualizacao = DateTime.Now;
            BitAtivo = input.BitAtivo;

            if (input.Cargos != null)
            {
                var cargos = input.Cargos.Select(x => new Cargo(x.Nome, x.TipoCargo, x.GestaoDe, x.GestaoAte));
                CargosSerializado = JsonConvert.SerializeObject(cargos);
            }
            else
            {
                CargosSerializado = null;
            }

            if (input.Clubes != null)
            {
                var clubes = input.Clubes.Select(x => new SocioClube(x.NumeroDistrito, x.Nome, x.Posse, x.Desligamento));
                CargosSerializado = JsonConvert.SerializeObject(clubes);
            }
            else
            {
                CargosSerializado = null;
            }

            PartitionKey = Codigo;
        }

        public void Atualizar(CadastroSocioInput input)
        {
            Codigo = input.Codigo;
            Nome = input.Nome;
            Apelido = input.Apelido;
            DataNascimento = input.DataNascimento;
            Email = input.Email;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Celular = input.Celular;
            Foto = input.Foto;
            DataAtualizacao = DateTime.Now;

            if (input.Cargos != null)
            {
                var cargos = input.Cargos.Select(x => new Cargo(x.Nome, x.TipoCargo, x.De, x.Ate));
                CargosSerializado = JsonConvert.SerializeObject(cargos);
            }
            else
            {
                CargosSerializado = null;
            }

            if (input.Clubes != null)
            {
                var clubes = input.Clubes.Select(x => new SocioClube(x.NumeroDistrito, x.NomeClube, x.Posse, x.Desligamento));
                CargosSerializado = JsonConvert.SerializeObject(clubes);
            }
            else
            {
                CargosSerializado = null;
            }

            PartitionKey = Codigo;
        }

        public void Inativar()
        {
            BitAtivo = false;
            DataAtualizacao = DateTime.Now;
        }
    }
}
