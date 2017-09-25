using DAL;
using DAL.Interfaces;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    [Route("Quotes")]
    public class QuotesController : Controller
    {

        private IUnitOfWork unitOfWork = null;

        public QuotesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {   
            return View();
        }
    }
}
