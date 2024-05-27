using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dto
{
    public class UtenteDTOperPOST
    {
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public DateOnly DataNascita { get; set; }
        public string CittaNascita { get; set; }
        public string ProvinciaNascita { get; set; }
        public string Sesso { get; set; }
        public string CodiceFiscale { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
