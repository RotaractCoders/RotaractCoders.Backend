using Domain.Commands.Inputs;
using Domain.Contracts.Commands;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Enum;
using FluentValidator;

namespace Domain.Commands.Handlers
{
    public class CadastroCargoSocioHandler : Notifiable,
        ICommandHandler<CadastrarCargoSocioInput>
    {
        private ICargoRepository _cargoRepository;
        private ICargoClubeRepository _cargoClubeRepository;
        private IClubeRepository _clubeRepository;
        private ISocioRepository _socioRepository;

        public CadastroCargoSocioHandler(
            ICargoRepository cargoRepository, 
            ICargoClubeRepository cargoClubeRepository, 
            IClubeRepository clubeRepository,
            ISocioRepository socioRepository)
        {
            _cargoRepository = cargoRepository;
            _cargoClubeRepository = cargoClubeRepository;
            _clubeRepository = clubeRepository;
            _socioRepository = socioRepository;
        }

        public ICommandResult Handle(CadastrarCargoSocioInput command)
        {
            //var socio = _socioRepository.Buscar(command.CodigoSocio);

            //_cargoClubeRepository.ListarPorSocio(socio.Id).ForEach(x =>
            //{
            //    _cargoClubeRepository.Excluir(x.Id);
            //});

            //command.Lista.ForEach(input =>
            //{
            //    var cargo = _cargoRepository.Buscar(input.Cargo, TipoCargo.Clube);

            //    if (cargo == null)
            //    {
            //        cargo = _cargoRepository.Incluir(new Cargo(input.Cargo, TipoCargo.Clube));
            //    }

            //    var clube = _clubeRepository.BuscarPorNome(input.Clube);

            //    //var cargoClube = new CargoClube(socio.Id, cargo.Id, clube.Id, input.De, input.Ate);
            //    //_cargoClubeRepository.Incluir(cargoClube);
            //});

            return null;
        }
    }
}
