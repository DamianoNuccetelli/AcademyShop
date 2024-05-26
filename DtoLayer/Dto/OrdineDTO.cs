using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dto
{
    public class OrdineDTO
    {
        public int Id { get; set; }
        public int FkIdUtente { get; set; }
        public DateTime DataRegistrazione { get; set; }
        public DateTime? DataAggiornamento { get; set; }
        public int FkIdStato { get; set; }
    }
}
