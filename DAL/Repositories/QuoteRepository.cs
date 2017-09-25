using DAL.Model;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;


namespace DAL.Repository 
{
    public class QuoteRepository : IQuoteRepository
    {

        private MovieBuffContext context = null;
        private bool disposed = false;

        public QuoteRepository(MovieBuffContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("missing context");
            }

            this.context = context;
        }

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