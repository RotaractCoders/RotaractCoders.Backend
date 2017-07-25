﻿using System;

namespace Domain.Entities
{
    public class Tarefa
    {
        public long Id { get; private set; }
        public DateTime? Data { get; private set; }
        public string Descricao { get; private set; }
        public long IdProjeto { get; private set; }
        public Projeto Projeto { get; private set; }
    }
}