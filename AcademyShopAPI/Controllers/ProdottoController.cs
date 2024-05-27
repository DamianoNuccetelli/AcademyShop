using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademyShopAPI.Models;

namespace AcademyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottoController : ControllerBase
    {
        private readonly BusinessLayer.ManageBusiness _oBL;

        public ProdottoController(BusinessLayer.ManageBusiness oBL)
        {
            _oBL = oBL;
        }
   

       

    }
}
