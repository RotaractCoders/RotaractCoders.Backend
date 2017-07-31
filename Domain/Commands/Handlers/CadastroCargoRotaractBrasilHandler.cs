using Domain.Commands.Inputs;
using Domain.Contracts.Commands;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Enum;
using FluentValidator;
using System;

namespace Domain.Commands.Handlers
{
    public class CadastroCargoRotaractBrasilHandler : Notifiable,
        ICommandHandler<CadastroCargoRotaractBrasilInput>
    {
        private ICargoRepository _cargoRepository;
        private ISocioRepository _socioRepository;
        private IDistritoRepository _distritoRepository;
        private ICargoRotaractBrasilRepository _cargoRotaractBrasilRepository;

        public CadastroCargoRotaractBrasilHandler(
            ICargoRepository cargoRepository,
            ISocioRepository socioRepository,
            IDistritoRepository distritoRepository,
            ICargoRotaractBrasilRepository cargoRotaractBrasilRepository)
        {
            _cargoRepository = cargoRepository;
            _socioRepository = socioRepository;
            _distritoRepository = distritoRepository;
            _cargoRotaractBrasilRepository = cargoRotaractBrasilRepository;
        }

        public ICommandResult Handle(CadastroCargoRotaractBrasilInput command)
        {
            var socio = _socioRepository.Buscar(command.CodigoSocio);

            _cargoRotaractBrasilRepository.ListarPorSocio(socio.Id).ForEach(x =>
            {
                _cargoRotaractBrasilRepository.Excluir(x.Id);
            });

            command.Lista.ForEach(input =>
            {
                var cargo = _cargoRepository.Buscar(input.Cargo, TipoCargo.RotaractBrasil);

                if (cargo == null)
                {
                    cargo = _cargoRepository.Incluir(new Cargo(input.Cargo, TipoCargo.RotaractBrasil));
                }

                var cargoRotaractBrasil = new CargoRotaractBrasil(socio.Id, cargo.Id, input.De, input.Ate);
                _cargoRotaractBrasilRepository.Incluir(cargoRotaractBrasil);
            });

            return null;
        }
    }
}
