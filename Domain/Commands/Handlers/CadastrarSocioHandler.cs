using Domain.Commands.Inputs;
using Domain.Contracts.Commands;
using Domain.Contracts.Repositories;
using Domain.Entities;
using FluentValidator;

namespace Domain.Commands.Handlers
{
    public class CadastrarSocioHandler : Notifiable,
        ICommandHandler<CadastroSocioInput>
    {
        private ISocioRepository _socioRepository;

        public CadastrarSocioHandler(ISocioRepository socioRepository)
        {
            _socioRepository = socioRepository;
        }

        public ICommandResult Handle(CadastroSocioInput input)
        {
            var socioCadastrado = _socioRepository.Buscar(input.Codigo);

            if (socioCadastrado == null)
            {
                socioCadastrado = new Socio(input);

                _socioRepository.Incluir(socioCadastrado);
            }
            else
            {
                socioCadastrado.Atualizar(input);

                _socioRepository.Atualizar(socioCadastrado);
            }

            return null;
        }
    }
}
