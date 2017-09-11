using DAL.Model;
using DAL.Interfaces;

using System;
using System.Collections.Generic;

namespace DAL.Repository 
{
    public class QuoteRepository : IQuoteRepository
    {
        public IEnumerable<Quote> GetQuotesByMovie(int movieId) {
            throw new NotImplementedException();
        }

        public IEnumerable<Quote> GetQuotesByCharacter(int characterId) {
            throw new NotImplementedException();
        }

        public  Quote AddQuote(Quote quote) {

            throw new NotImplementedException();
        }

        public void Save() {

            throw new NotImplementedException();
        }

    }
}