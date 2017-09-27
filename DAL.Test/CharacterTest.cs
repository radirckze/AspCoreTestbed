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
        static IUnitOfWork unitOfWork = null;
        private static IGenericRepository<Movie> movieRepo = null;
        private static ICharacterRepository characterRepo = null;
        static Movie jPark = null;
        static Mcharacter jHammond = null;

        [ClassInitialize]
        public static void TestInit(TestContext tc)
        {
            unitOfWork = new UnitOfWork();
            characterRepo = unitOfWork.CharacterRepository;
            movieRepo = unitOfWork.MovieRepository;

            jPark = movieRepo.AddEntity(new Movie() { Name = "Jurassic Park", Rating = 4 });
            jHammond = new Mcharacter() { Name = "John Hammond" };
            characterRepo.AddCharacter(jHammond);
            unitOfWork.Save();

        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            movieRepo.DeleteEntity(jPark.Id);
            characterRepo.DeleteCharacter(jHammond.Id);
            unitOfWork.Save();
        }

        [TestMethod]
        public void GetCharacters()
        {
            IEnumerable<Mcharacter> characters = characterRepo.GetCharacters();
            Assert.IsTrue(characters != null && characters.Count() > 0);
        }

        [TestMethod]
        public void AddCharacter()
        {
            bool testStatus = false;
            Mcharacter iMalcolm = new Mcharacter() { Name = "Ian Malcolm" };
            iMalcolm = characterRepo.AddCharacter(iMalcolm);
            unitOfWork.Save();
            if (iMalcolm.Id > 0)
            {
                testStatus = true;
                characterRepo.DeleteCharacter(iMalcolm.Id);
                unitOfWork.Save();
            }
            Assert.IsTrue(testStatus);
        }

        [TestMethod]
        public void DeleteCharacter()
        {
            bool testStatus = false;
            string eSattlerName = "Ellie Sattler";
            Mcharacter eSattler = new Mcharacter() { Name = eSattlerName };
            eSattler = characterRepo.AddCharacter(eSattler);
            unitOfWork.Save();
            if (eSattler.Id > 0)
            {
                testStatus = true;
                characterRepo.DeleteCharacter(eSattler.Id);
                unitOfWork.Save();
                testStatus = testStatus & (characterRepo.GetCharacterByName(eSattlerName) == null);
            }
            Assert.IsTrue(testStatus);
        }


    }
}
