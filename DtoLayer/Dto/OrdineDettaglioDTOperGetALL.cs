using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dto
{
    public class OrdineDettaglioDTOperGetALL
    {
        public string ProdottoNome { get; set; }
        public string ProdottoDescrizione { get; set; }
        public string StatoOrdineDescrizione { get; set; }
        public int Quantita { get; set; }
        public int ProdottoId { get; set; }
        public DateTime DataRegistrazione { get; set; }
        public DateTime? DataAggiornamento { get; set; }
        public int IdDettaglioOrdine { get; set; }

    }
}
