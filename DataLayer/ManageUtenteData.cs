using AcademyShopAPI.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ManageUtenteData
    {
        private readonly AcademyShopDBContext _context;
        private readonly IRepositoryAsync<Utente> _utenteRepository;

        public ManageUtenteData(AcademyShopDBContext context, IRepositoryAsync<Utente> utenteRepository)
        {
            _context = context;
            _utenteRepository = utenteRepository;
        }

        //DANIEL CON REPOSITORY
        public async Task<ActionResult<Utente>> AddUtenteAsync(Utente utente)
        {
            try
            {
                utente.DataRegistrazione = DateTime.UtcNow;
                await _utenteRepository.AddAsync(utente);
                return new CreatedAtRouteResult(nameof(GetUtente), new { id = utente.Id }, utente);
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la creazione dell'utente.", ex);
            }
        }

        public async Task<ActionResult<Utente>> DeleteUtente(int id)
        {
            try
            {
                //var utente = await _context.Utentes.FindAsync(id);
                var utente = await _utenteRepository.GetByIdAsync(id);
                if (utente == null)
                {
                    {
                        return new NotFoundObjectResult("Utente non trovato.");
                    }
                }

                await _utenteRepository.DeleteAsync(id);

                return new OkObjectResult($"Utente '{utente.Nome} {utente.Cognome}' eliminato con successo.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante l'eliminazione dell'utente con ID {id} nel livello dei dati.", ex);
            }
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



        // Leonardo
        public async Task<Utente> LoginUser(string email, string password)
        {
            try
            {
                Utente utente = await _context.Utentes.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                return utente;
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }


        private ContentResult ErrorContentResult(string errorMessage, int statusCode = 400)
        {
            return new ContentResult
            {
                Content = errorMessage,
                ContentType = "text/plain",
                StatusCode = statusCode
            };
        }

    }
}
