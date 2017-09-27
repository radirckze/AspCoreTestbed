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
    public class QuotesTest
    {
        //private MovieBuffContext context = null;
        static IUnitOfWork unitOfWork = null;
        private static ICharacterRepository characterRepo = null;
        private static IGenericRepository<Movie> movieRepo = null;
        private static IQuoteRepository quoteRepo = null;
        static Movie jPark = null, jParkLW = null;
        static Mcharacter jHammond = null, iMalcolm = null;
        static AppearsIn appears1 = null, appears2 = null, appears3 = null, appears4 = null;
        static Quote quote1 = null, quote2 = null, quote3 = null;

        [ClassInitialize]
        public static void TestInit(TestContext tc)
        {
            unitOfWork = new UnitOfWork();
            characterRepo = unitOfWork.CharacterRepository;
            movieRepo = unitOfWork.MovieRepository;
            quoteRepo = unitOfWork.QuoteRepository;

            jPark = movieRepo.AddEntity(new Movie() { Name = "Jurassic Park", Rating = 4 });
            jParkLW = movieRepo.AddEntity(new Movie() { Name = "Jurassic Park: The Lost World", Rating = 4 });

            jHammond = characterRepo.AddCharacter(new Mcharacter() { Name = "John Hammond" });
            iMalcolm = characterRepo.AddCharacter(new Mcharacter() { Name = "Ian Malcolm" });

            appears1 = characterRepo.AddCharacteAppearsIn(jHammond.Id, jPark.Id);
            appears2 = characterRepo.AddCharacteAppearsIn(jHammond.Id, jParkLW.Id);
            appears3 = characterRepo.AddCharacteAppearsIn(iMalcolm.Id, jPark.Id);
            appears4 = characterRepo.AddCharacteAppearsIn(iMalcolm.Id, jParkLW.Id);

            string quoteStr = @"Our scientists have done things which nobody's ever done before";
            quote1 = quoteRepo.AddQuote(new Quote() { CharacterId = jHammond.Id, MovieId = jPark.Id, Quote1 = quoteStr });

            quoteStr = @"... your scientists were so preoccupied with whether or not they could that they did
                nor atop to think if they should";
            quote2 = quoteRepo.AddQuote(new Quote() { CharacterId = iMalcolm.Id, MovieId = jPark.Id, Quote1 = quoteStr });

            quoteStr = @"Don't worry. I am not making the same mistake again.";
            quote3 = quoteRepo.AddQuote(new Quote() { CharacterId = jHammond.Id, MovieId = jParkLW.Id, Quote1 = quoteStr });

            unitOfWork.Save();

        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            quoteRepo.DeleteQuote(quote1.Id);
            quoteRepo.DeleteQuote(quote2.Id);
            quoteRepo.DeleteQuote(quote3.Id);
            characterRepo.DeleteCharacteAppearsIn(appears1.CharacterId, appears1.MovieId);
            characterRepo.DeleteCharacteAppearsIn(appears2.CharacterId, appears2.MovieId);
            characterRepo.DeleteCharacteAppearsIn(appears3.CharacterId, appears3.MovieId);
            characterRepo.DeleteCharacteAppearsIn(appears4.CharacterId, appears4.MovieId);
            characterRepo.DeleteCharacter(jHammond.Id);
            characterRepo.DeleteCharacter(iMalcolm.Id);
            movieRepo.DeleteEntity(jPark.Id);
            movieRepo.DeleteEntity(jParkLW.Id);
            unitOfWork.Save();
        }

        [TestMethod]
        public void GetQuotesByMovie()
        {
            IEnumerable<Quote> quotes = quoteRepo.GetQuotesByMovie(jPark.Id);
            Assert.IsTrue(quotes != null && quotes.Count() == 2);
        }

        [TestMethod]
        public void GetQuotesByCharacter()
        {
            IEnumerable<Quote> quotes = quoteRepo.GetQuotesByCharacter(iMalcolm.Id);
            Assert.IsTrue(quotes != null && quotes.Count() == 1);
        }
    }
}
