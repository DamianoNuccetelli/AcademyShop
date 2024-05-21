﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AcademyShopAPI.Models;

public partial class AcademyShopDBContext : DbContext
{
    public AcademyShopDBContext()
    {
    }

    public AcademyShopDBContext(DbContextOptions<AcademyShopDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DettaglioOrdine> DettaglioOrdines { get; set; }

    public virtual DbSet<Ordine> Ordines { get; set; }

    public virtual DbSet<Prodotto> Prodottos { get; set; }

    public virtual DbSet<StatoOrdine> StatoOrdines { get; set; }

    public virtual DbSet<Utente> Utentes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=841XLQ2;Initial Catalog=AcademyShopDB;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DettaglioOrdine>(entity =>
        {
            entity.HasOne(d => d.FkIdOrdineNavigation).WithMany(p => p.DettaglioOrdines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dettaglio_Ordine_Ordine");

            entity.HasOne(d => d.FkIdProdottoNavigation).WithMany(p => p.DettaglioOrdines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dettaglio_Ordine_Prodotto");
        });

        modelBuilder.Entity<Ordine>(entity =>
        {
            entity.HasOne(d => d.FkIdStatoNavigation).WithMany(p => p.Ordines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordine_Stato_Ordine");

            entity.HasOne(d => d.FkIdUtenteNavigation).WithMany(p => p.Ordines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordine_Utente");
        });

        modelBuilder.Entity<Utente>(entity =>
        {
            entity.Property(e => e.CodiceFiscale).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}