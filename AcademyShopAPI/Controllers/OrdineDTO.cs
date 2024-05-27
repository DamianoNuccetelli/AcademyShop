
namespace AcademyShopAPI.Controllers
{
    public class OrdineDTO
    {
        public DateTime DataRegistrazione { get; set; }
        public DateTime? DataAggiornamento { get; set; }
        public string DescrizioneStato { get; set; }
        public int IDProdotto { get; set; }
        public string DescrizioneProdotto { get; set; }
        public int Quantita { get; set; }
    }
}