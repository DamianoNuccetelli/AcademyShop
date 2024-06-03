﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AcademyShopAPI.Models;

[Table("Ordine")]
public partial class Ordine
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("FK_ID_Utente")]
    public int FkIdUtente { get; set; }

    [Column("Data_Registrazione", TypeName = "datetime")]
    public DateTime DataRegistrazione { get; set; }

    [Column("Data_Aggiornamento", TypeName = "datetime")]
    public DateTime? DataAggiornamento { get; set; }

    [Column("FK_ID_Stato")]
    public int FkIdStato { get; set; }

    [InverseProperty("FkIdOrdineNavigation")]
    public virtual ICollection<DettaglioOrdine> DettaglioOrdines { get; set; } = new List<DettaglioOrdine>();

    [ForeignKey("FkIdStato")]
    [InverseProperty("Ordines")]
    public virtual StatoOrdine FkIdStatoNavigation { get; set; }

    [ForeignKey("FkIdUtente")]
    [InverseProperty("Ordines")]
    public virtual Utente FkIdUtenteNavigation { get; set; }

    //costruttori
    public Ordine() { }
    public Ordine(int FkIdUtente, int FkIdStato, DateTime DataRegistrazione) {
        this.FkIdUtente = FkIdUtente;
        this.FkIdStato= FkIdStato;
        this.DataRegistrazione = DataRegistrazione;
    }
}