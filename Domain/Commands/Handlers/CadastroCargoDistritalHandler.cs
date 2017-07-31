using Domain.Commands.Inputs;
using Domain.Contracts.Commands;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Enum;
using FluentValidator;

namespace Domain.Commands.Handlers
{
    public class CadastroCargoDistritalHandler : Notifiable,
        ICommandHandler<CadastroCargoDistritoInput>
    {
        private ICargoRepository _cargoRepository;
        private ISocioRepository _socioRepository;
        private ICargoDistritoRepository _cargoDistritoRepository;
        private IDistritoRepository _distritoRepository;

        public CadastroCargoDistritalHandler(
            ICargoRepository cargoRepository,
            ISocioRepository socioRepository,
            ICargoDistritoRepository cargoDistritoRepository,
            IDistritoRepository distritoRepository)
        {
            _cargoRepository = cargoRepository;
            _socioRepository = socioRepository;
            _cargoDistritoRepository = cargoDistritoRepository;
            _distritoRepository = distritoRepository;
        }

        public ICommandResult Handle(CadastroCargoDistritoInput command)
        {
            var socio = _socioRepository.Buscar(command.CodigoSocio);

            _cargoDistritoRepository.ListarPorSocio(socio.Id).ForEach(x =>
            {
                _cargoDistritoRepository.Excluir(x.Id);
            });

            command.Lista.ForEach(input =>
            {
                var cargo = _cargoRepository.Buscar(input.Cargo, TipoCargo.Distrital);

                if (cargo == null)
                {
                    cargo = _cargoRepository.Incluir(new Cargo(input.Cargo, TipoCargo.Distrital));
                }

                var distrito = _distritoRepository.Buscar(input.Distrito);

                var cargoDistrito = new CargoDistrito(socio.Id, distrito.Id, cargo.Id, input.De, input.Ate);
                _cargoDistritoRepository.Incluir(cargoDistrito);
            });

            return null;
        }
    }
}
