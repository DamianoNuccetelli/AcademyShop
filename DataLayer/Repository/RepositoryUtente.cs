using AcademyShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
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
