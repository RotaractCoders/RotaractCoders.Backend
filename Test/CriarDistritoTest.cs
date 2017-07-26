using Domain.Commands.Handlers;
using Domain.Commands.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Repositories;

namespace Test
{
    [TestClass]
    public class CriarDistritoTest
    {
        [TestMethod]
        public void CriarDistrito()
        {
            var repository = new BaseRepository();
            var handler = new CriarDistritoHandler(repository.DistritoRepository);

            var result = handler.Handle(new CriarDistritoInput
            {
                Email = "4430@hotmail.com",
                Mascote = "Hulk",
                Numero = "4430",
                Regiao = 5,
                Site = "www.rotaract4430.org"
            });

            Assert.IsTrue(handler.IsValid());
        }
    }
}
