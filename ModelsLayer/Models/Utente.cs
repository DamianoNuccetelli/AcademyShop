﻿
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AcademyShopAPI.Models;

[Table("Utente")]
public partial class Utente
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Cognome { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Nome { get; set; }

    [Column("Data_Nascita")]
    public DateOnly DataNascita { get; set; }

    [Required]
    [Column("Citta_Nascita")]
    [StringLength(50)]
    [Unicode(false)]
    public string CittaNascita { get; set; }

    [Required]
    [Column("Provincia_Nascita")]
    [StringLength(2)]
    [Unicode(false)]
    public string ProvinciaNascita { get; set; }

    [Required]
    [StringLength(1)]
    [Unicode(false)]
    public string Sesso { get; set; }

    [Required]
    [Column("Codice_Fiscale")]
    [StringLength(16)]
    [Unicode(false)]
    public string CodiceFiscale { get; set; }

    [Column("Data_Registrazione", TypeName = "datetime")]
    public DateTime DataRegistrazione { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

    [Required]
    [StringLength(16)]
    [Unicode(false)]
    public string Password { get; set; }

    [InverseProperty("FkIdUtenteNavigation")]
    public virtual ICollection<Ordine> Ordines { get; set; } = new List<Ordine>();
}