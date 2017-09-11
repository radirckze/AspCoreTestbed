using DAL;
using DAL.Model;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace dal.test
{
    [TestClass]
    public class CharacterTest
    {
        //private MovieBuffContext context = null;
        IUnitOfWork unitOfWork = null;
        private IGenericRepository<Character> characterRepo = null;
        private IMovieRepository movieRepo = null;
        Movie jPark = null;
        Character jHammond = null;

        [TestInitialize]
        public void TestInit()
        {
            unitOfWork = new UnitOfWork();
            characterRepo = unitOfWork.CharacterRepository;
            movieRepo = unitOfWork.MovieRepository;

            jPark = movieRepo.AddMovie(new Movie() { Name = "Jurassic Park", Rating = 4 });
            jHammond = new Character() { Name = "John Hammond" };
            characterRepo.AddEntity(jHammond);
            unitOfWork.Save();

        }

        [TestCleanup]
        public void TestCleanup()
        {
            movieRepo.DeleteMovie(jPark.Id);
            characterRepo.DeleteEntity(jHammond.Id);
            unitOfWork.Save();
        }

        [TestMethod]
        public void GetCharacters()
        {
            IEnumerable<Character> characters = characterRepo.GetEntities();
            Assert.IsTrue(characters != null && characters.Count() > 0);
        }

        [TestMethod]
        public void AddCharacter()
        {
            bool testStatus = false;
            Character iMalcolm = new Character() { Name = "Ian Malcolm" };
            iMalcolm = characterRepo.AddEntity(iMalcolm);
            unitOfWork.Save();
            if (characterRepo.GetEntityById(iMalcolm.Id) != null)
            {
                testStatus = true;
                characterRepo.DeleteEntity(iMalcolm.Id);
                unitOfWork.Save();
            }
            Assert.IsTrue(testStatus);
        }

        [TestMethod]
        public void DeleteCharacter()
        {
            bool testStatus = false;
            Character eSattler = new Character() { Name = "Ellie Sattler" };
            eSattler = characterRepo.AddEntity(eSattler);
            unitOfWork.Save();
            if (characterRepo.GetEntityById(eSattler.Id) != null)
            {
                testStatus = true;
                characterRepo.DeleteEntity(eSattler.Id);
                unitOfWork.Save();
                testStatus = testStatus & (characterRepo.GetEntityById(eSattler.Id) == null);
            }
            Assert.IsTrue(testStatus);
        }


    }
}
