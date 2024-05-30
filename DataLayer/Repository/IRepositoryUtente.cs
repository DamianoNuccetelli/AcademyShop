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
    public interface IRepositoryUtente
    {
        //Daniel
        Task<ActionResult<Utente>> AddUtenteAsync(Utente utente);
        Task<ActionResult<Utente>> DeleteUtente(int id);
        Task<bool> CheckUtenteExists(Utente utente);
        Task<bool> CheckUtenteExistsById(int id);
        Task<IEnumerable<Utente>> GetUtentes();
        Task<Utente> GetUtente(int id);
        Task<IEnumerable<Utente>> GetAllAsync();
        Task<Utente> GetByIdAsync(int id);
        Task<Utente> AddAsync(Utente utente);
        Task<bool> DeleteAsync(int id);


    }
}
