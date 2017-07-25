using System;
using Domain.Commands.Inputs;
using Domain.Contracts.Commands;
using FluentValidator;
using Domain.Contracts.Repositories;

namespace Domain.Commands.Handlers
{
    public class CriarDistritoHandler : Notifiable,
        ICommandHandler<CriarDistritoInput>
    {
        private readonly IDistritoRepository _repository;

        public CriarDistritoHandler(IDistritoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CriarDistritoInput command)
        {
            throw new NotImplementedException();
        }
    }
}
