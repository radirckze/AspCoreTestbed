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
    [Route("Characters")]
    public class CharactersController : Controller
    {

         private IUnitOfWork unitOfWork = null;

        public CharactersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            ICharacterRepository characterRepo = unitOfWork.CharacterRepository;
            IEnumerable<Mcharacter> characters = characterRepo.GetCharacters();

            return View(characters);
        }

        [Route("")]
        [Route("Create")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Mcharacter character)
        //public IActionResult Movie(string name, int rating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICharacterRepository characterRepo = unitOfWork.CharacterRepository;
                    character = characterRepo.AddCharacter(character);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return RedirectToAction("Index");
        }
    }
}