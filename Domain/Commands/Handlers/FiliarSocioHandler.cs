using System;
using Domain.Commands.Inputs;
using Domain.Contracts.Commands;
using FluentValidator;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Domain.Commands.Handlers
{
    public class FiliarSocioHandler : Notifiable,
        ICommandHandler<FiliarSocioListInput>
    {
        private IDistritoRepository _distritoRepository;
        private ISocioRepository _socioRepository;
        private IClubeRepository _clubeRepository;
        private ISocioClubeRepository _socioClubeRepository;

        public FiliarSocioHandler(
            IDistritoRepository distritoRepository, 
            ISocioRepository socioRepository, 
            IClubeRepository clubeRepository,
            ISocioClubeRepository socioClubeRepository)
        {
            _distritoRepository = distritoRepository;
            _socioRepository = socioRepository;
            _clubeRepository = clubeRepository;
            _socioClubeRepository = socioClubeRepository;
        }

        public ICommandResult Handle(FiliarSocioListInput command)
        {
            //var socioAtual = _socioRepository.Buscar(command.CodigoSocio);
            //if (socioAtual == null)
            //{
            //    AddNotification("Socio", "Erro");
            //    return null;
            //}

            //_socioClubeRepository.BuscarPorSocio(socioAtual.Id).ForEach(x =>
            //{
            //    _socioClubeRepository.Excluir(x.Id);
            //});

            //for (int i = 0; i < command.Lista.Count; i++)
            //{
            //    var input = command.Lista[i];

            //    var socio = _socioRepository.Buscar(command.CodigoSocio);
            //    if (socio == null)
            //    {
            //        AddNotification("Socio", "Erro");
            //        break;
            //    }
                
            //    var clube = _clubeRepository.BuscarPorNome(input.NomeClube);
            //    if (clube == null)
            //    {
            //        AddNotification("Clube", "Erro");
            //        break;
            //    }

                //_socioClubeRepository.Incluir(new SocioClube(clube.Id, socio.Id, input.Posse, input.Desligamento));
            //}

            return null;
        }
    }
}
