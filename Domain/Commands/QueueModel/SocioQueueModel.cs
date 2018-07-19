using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.QueueModel
{
    public class SocioQueueModel
    {
        public string Email { get; set; }
        public string Codigo { get; set; }
        public string NumeroDistrito { get; set; }
        public string CodigoClube { get; set; }
    }
}
