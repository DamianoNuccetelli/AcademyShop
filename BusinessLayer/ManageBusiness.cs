﻿using AcademyShopAPI.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoLayer.Dto;

namespace BusinessLayer
{
    public class ManageBusiness
    {
        private readonly AcademyShopDBContext _context;

        private ManageData oDL = null;

        public ManageBusiness(AcademyShopDBContext context)
        {
            _context = context;
        }

        public async Task<int?> RecuperaIdOrdineAsync(int idUtente, int idDettaglioOrdine)
        {
            oDL = new ManageData(_context);
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID dell'ordine
                return await oDL.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'ID dell'ordine nel business layer.", ex);
            }
        }


        public async Task<int?> RecuperaStatoOrdineAsync(int idOrdineEsistente)
        {
            oDL = new ManageData(_context);
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID dell'ordine
                return await oDL.RecuperaStatoOrdineAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'ID dello stato dell'ordine nel business layer.", ex);
            }
        }

        public async Task<int?> RecuperaQuantitaProdottoAsync(int idOrdineEsistente)
        {
            oDL = new ManageData(_context);
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID dell'ordine
                return await oDL.RecuperaQuantitaProdottoAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero della quantita del prodotto nel business layer.", ex);
            }
        }

        public async Task<int?> RecuperaIdProdottoAsync(int idOrdineEsistente)
        {
            oDL = new ManageData(_context);
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID dell'ordine
                return await oDL.RecuperaIdProdottoAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'ID del prodotto nel business layer.", ex);
            }
        }

        public async Task<bool> ModificaOrdineAsync(int idOrdineEsistente, int idProdotto, int quantita)
        {
            oDL = new ManageData(_context);
            try
            {
                return await oDL.ModificaOrdineAsync(idOrdineEsistente, idProdotto, quantita);
            }
            catch (Exception ex)
            {
                // Gestisci l'errore, ad esempio registrandolo o rilanciandolo
                throw new Exception("Errore durante la modifica dell'ordine nel business layer.", ex);
            }
        }

    }
}