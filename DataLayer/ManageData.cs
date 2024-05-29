using AcademyShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Humanizer.Localisation;
using DataLayer.Repository;

namespace DataLayer
{
    public class ManageData
    {
        private readonly AcademyShopDBContext _context;
        private readonly IRepositoryAsync<Utente> _utenteRepository;


        public ManageData(AcademyShopDBContext context, IRepositoryAsync<Utente> utenteRepository)
        {
            _context = context;
            _utenteRepository = utenteRepository;
        }



       
    }
}
