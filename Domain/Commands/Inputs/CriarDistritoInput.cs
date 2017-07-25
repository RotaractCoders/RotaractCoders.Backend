using Domain.Contracts.Commands;

namespace Domain.Commands.Inputs
{
    public class CriarDistritoInput : ICommand
    {
        public string Numero { get; set; }
        public int Regiao { get; set; }
        public string Mascote { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
    }
}
