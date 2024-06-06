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
        private readonly IRepositoryOrdine repositoryOrdine;
        public RepositoryUtente(AcademyShopDBContext context,IRepositoryOrdine repositoryOrdine)
        {
            _context = context;
            this.repositoryOrdine = repositoryOrdine;
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
        public Utente GetUtenteByIdSync(int id)
        {
            return _context.Utentes.Find(id);
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
        public ActionResult<Utente> DeleteUtenteSync(int id)
        {
            var utente =  GetUtenteByIdSync(id);

            _context.Utentes.Remove(utente);
            _context.SaveChanges();

            return utente;
        }
    
        public ActionResult<Utente> DeleteUtenteEOrdine(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var utente = GetUtenteByIdSync(id);
                
                    List<Ordine> ordineList = repositoryOrdine.GetOrdiniByUserIdSync(id);

                    List<DettaglioOrdine> dettaglioOrdineList = repositoryOrdine.GetDettaglioOrdineSync(ordineList);

                    if (dettaglioOrdineList.Any())
                    {
                        _context.DettaglioOrdines.RemoveRange(dettaglioOrdineList);
                    }

                    if (ordineList.Any())
                    {
                        _context.Ordines.RemoveRange(ordineList);
                    }

                    _context.Utentes.Remove(utente);
                    _context.SaveChanges();

                    transaction.Commit();

                    return utente;
                }
                catch (Exception)
                {
                    transaction.Rollback();
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
