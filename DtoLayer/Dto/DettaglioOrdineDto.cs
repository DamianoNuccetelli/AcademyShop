public class DettaglioOrdineDto
{
    public int Id { get; set; }
    public int FkIdOrdine { get; set; }
    public int FkIdProdotto { get; set; }
    public int Quantita { get; set; }
    public DateTime DataRegistrazione { get; set; }
    public DateTime? DataAggiornamento { get; set; }
    public string? StatoOrdineDescrizione{ get; set; }
    public string? ProdottoDescrizione { get; set; }
}
