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
        public async Task<ActionResult<Utente>> AddUtenteAsync(Utente utente)
        {
                //Imposto la data di registrazione a quella attuale
                utente.DataRegistrazione = DateTime.UtcNow;

                await AddAsync(utente);
                return new CreatedAtRouteResult(nameof(GetUtente), new { id = utente.Id }, utente);

                // var result = await _utenteRepository.AddAsync(utente);
                // return new CreatedAtRouteResult(nameof(GetUtente), new { id = createdUtente.Id }, result);
        }


        public async Task<ActionResult<Utente>> DeleteUtente(int id)
        {
                var utente = await GetByIdAsync(id);

                await DeleteAsync(id);
                //var result = await _utenteRepository.DeleteAsync(id);

            return new OkObjectResult($"Utente '{utente.Nome} {utente.Cognome}' eliminato con successo.");
        }


        public async Task<bool> CheckUtenteExists(Utente utente)
        {
            return await _context.Utentes.AnyAsync(u => u.CodiceFiscale == utente.CodiceFiscale || u.Email == utente.Email);
        }

        public async Task<bool> CheckUtenteExistsById(int id)
        {
            return await _context.Utentes.AnyAsync(u => u.Id == id);
        }
        //Daniel -> Aggiunta e rimozione dell'utente dal db
        public async Task<IEnumerable<Utente>> GetUtentes()
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

        public async Task<Utente> GetUtente(int id)
        {
            try
            {
                return await _context.Utentes.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante il recupero dell'utente con ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<Utente>> GetAllAsync()
        {
            return await _context.Utentes.ToListAsync();
        }

        public async Task<Utente> GetByIdAsync(int id)
        {
            return await _context.Utentes.FindAsync(id);
        }

        public async Task<Utente> AddAsync(Utente utente)
        {
            if (utente == null)
            {
                throw new ArgumentNullException(nameof(utente));
            }

            _context.Set<Utente>().Add(utente);
            await _context.SaveChangesAsync();
            return utente;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Utentes.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }




    }
}
