using DtoLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRepositoryGetOrdini
    {
        Task<OrdineDettaglioDTOperGET> GetOrdineDettaglioAsync(int userId, int dettaglioOrdineId);
        Task<string?> GetUserPassword(int userId);
        Task<bool> VerificaUserAsync(int userId, string password);
    }

}
