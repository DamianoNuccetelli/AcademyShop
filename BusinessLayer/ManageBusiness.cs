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
        private readonly DataLayer.ManageData _oData;

        public ManageBusiness(DataLayer.ManageData oData)
        {
            _oData = oData;
        }
        public async Task<OrdineDettaglioDTOperGET> GetOrdineDettaglioAsync(int userId, int dettaglioOrdineId)
        {
            var result = await _oData.GetOrdineDettaglioAsync(userId, dettaglioOrdineId);

            if (result == null)
            {
                throw new ApplicationException("Errore nell'esecuzione dell'operazione");
            }

            return result;
        }
    }
}
