using Domain.Commands.Inputs;
using Domain.Contracts.Commands;
using Domain.Contracts.Repositories;
using Domain.Entities;
using FluentValidator;

namespace Domain.Commands.Handlers
{
    public class CriarClubeHandler : Notifiable,
        ICommandHandler<CriarClubeInput>
    {
        private IClubeRepository _clubeRepository;
        private IDistritoRepository _distritoRepository;

        public CriarClubeHandler(IClubeRepository clubeRepository, IDistritoRepository distritoRepository)
        {
            _clubeRepository = clubeRepository;
            _distritoRepository = distritoRepository;
        }

        public ICommandResult Handle(CriarClubeInput input)
        {
            var clubeCadastrado = _clubeRepository.Buscar(input.Codigo);

            var distrito = _distritoRepository.Buscar(input.numeroDistrito);

            if (distrito == null)
            {
                AddNotification("numeroDistrito", "O número do distrito é obrigatório");
                return null;
            }

            if (clubeCadastrado == null)
            {
                clubeCadastrado = new Clube(input, distrito.Id);

                _clubeRepository.Incluir(clubeCadastrado);
            }
            else
            {
                clubeCadastrado.Atualizar(input, distrito.Id);

                _clubeRepository.Atualizar(clubeCadastrado);
            }

            return null;
        }
    }
}
