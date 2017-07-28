using System;
using Domain.Commands.Inputs;
using Domain.Contracts.Commands;
using FluentValidator;
using Domain.Contracts.Repositories;
using Domain.Entities;

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

        public ICommandResult Handle(CriarDistritoInput input)
        {
            var distrito = new Distrito(input);

            if (!IsValid())
                return null;

            var distritoCadastrado = _repository.Buscar(distrito.Numero);
            
            if (distritoCadastrado == null)
            {
                distritoCadastrado = _repository.Incluir(distrito);
            }
            else
            {
                distritoCadastrado.Atualizar(input.Regiao, input.Mascote, input.Site, input.Email);

                if (!IsValid())
                    return null;

                _repository.Atualizar(distritoCadastrado);
            }

            return null;
        }
    }
}
