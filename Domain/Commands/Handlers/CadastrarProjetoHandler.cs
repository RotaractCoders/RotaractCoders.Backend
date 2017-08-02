using Domain.Commands.Inputs;
using Domain.Contracts.Commands;
using FluentValidator;
using System;

namespace Domain.Commands.Handlers
{
    public class CadastrarProjetoHandler : Notifiable,
        ICommandHandler<CadastrarProjetoInput>
    {


        public ICommandResult Handle(CadastrarProjetoInput command)
        {
            throw new NotImplementedException();
        }
    }
}
