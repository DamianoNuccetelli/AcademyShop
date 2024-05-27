using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dto
{
    public class OrdiniByIdUserDTO
    {
        public DateTime DataRegistrazione { get; set; }
        public DateTime? DataAggiornamento { get; set; }
        public string DescrizioneStato { get; set; }
        public int IDProdotto { get; set; }
        public string DescrizioneProdotto { get; set; }
        public int Quantita { get; set; }
    }

}
