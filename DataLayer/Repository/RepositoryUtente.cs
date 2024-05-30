using AcademyShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class RepositoryUtente : IRepositoryUtente
    {
        private readonly AcademyShopDBContext _context;

        public RepositoryUtente(AcademyShopDBContext context)
        {
            _context = context;
        }

        //DANIEL CON REPOSITORY
        //Aggiunta e rimozione dell'utente dal db
        public async Task<IEnumerable<Utente>> GetUtentesAsync()
        {
            try
            {
                return await _context.Utentes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero degli utenti.", ex);
            }
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
            var utente = await _context.Utentes.FindAsync(id);

            _context.Utentes.Remove(utente);
            await _context.SaveChangesAsync();

            return utente;
        }

        public async Task<bool> CheckUtenteExistsByEmailOrPassword(Utente utente)
        {
            return await _context.Utentes.AnyAsync(u => u.CodiceFiscale == utente.CodiceFiscale || u.Email == utente.Email);
        }


        public async Task<bool> CheckUtenteExistsById(int id)
        {
            return await _context.Utentes.AnyAsync(u => u.Id == id);
        }







    }
}
