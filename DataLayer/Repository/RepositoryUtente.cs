using AcademyShopAPI.Models;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLayer.Repository
{
    public class RepositoryUtente : IRepositoryUtente
    {
        private readonly AcademyShopDBContext _context;
       // private readonly IRepositoryOrdine iRepOrdine;

        public RepositoryUtente(AcademyShopDBContext context/*, IRepositoryOrdine iRepOrdine*/)
        {
            _context = context;
          //  this.iRepOrdine = iRepOrdine;
        }

        //DANIEL 
        public async Task<IEnumerable<Utente>> GetUtentesAsync()
        {
            return await _context.Utentes.ToListAsync();
        }

        public async Task<Utente> GetUtenteByIdAsync(int id)
        {
            return await _context.Utentes.FindAsync(id);
        }

        public async Task<ActionResult<Utente>> AddUtenteAsync(Utente utente)
        {
            //Imposto la data di registrazione a quella attuale
            utente.DataRegistrazione = DateTime.UtcNow;

            _context.Utentes.Add(utente);
            await _context.SaveChangesAsync();

            return utente;
        }


        public async Task<ActionResult<Utente>> DeleteUtenteAsync(int id)
        {
            var utente = await GetUtenteByIdAsync(id);

            _context.Utentes.Remove(utente);
            await _context.SaveChangesAsync();

            return utente;
        }

        public async Task<ActionResult<Utente>> DeleteUtenteEOrdineAsync(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var utente = await GetUtenteByIdAsync(id);
                    // Vengono prelevati gli ordini correlati all'utente
                    var ordineList = await _context.Ordines.Where(ordine => ordine.FkIdUtente == utente.Id).ToListAsync();

                    // Vengono prelevati i dettagliOrdine correlati all'ordine
                    var dettaglioOrdineList = await _context.DettaglioOrdines.Where(dettaglioOrdine => (ordineList.Select(o => o.Id).ToList()).Contains(dettaglioOrdine.FkIdOrdine)).ToListAsync();

                    if (dettaglioOrdineList.Any())
                    {
                        _context.DettaglioOrdines.RemoveRange(dettaglioOrdineList);
                    }

                    if (ordineList.Any())
                    {
                        _context.Ordines.RemoveRange(ordineList);
                    }

                    _context.Utentes.Remove(utente);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return utente;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new TransactionAbortedException();
                }
            }
        }



        public async Task<bool> CheckUtenteExistsByEmailOrPassword(Utente utente)
        {
            return await _context.Utentes.AnyAsync(u => u.CodiceFiscale == utente.CodiceFiscale || u.Email == utente.Email);
        }


        public async Task<bool> CheckUtenteExistsById(int id)
        {
            return await _context.Utentes.AnyAsync(u => u.Id == id);
        }


        // Leonardo
        public async Task<Utente> LoginUser(string email, string password)
        {
            try
            {
                Utente? utente = await _context.Utentes.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
#pragma warning disable CS8603
                return utente;
#pragma warning restore CS8603
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }


    }
}
