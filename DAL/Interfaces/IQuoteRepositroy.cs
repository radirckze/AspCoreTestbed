using DAL.Model;

using System;
using System.Collections.Generic;

namespace DAL.Interfaces 
{
    public interface IQuoteRepository
    {
        IEnumerable<Quote> GetQuotesByMovie(int movieId);
        IEnumerable<Quote> GetQuotesByCharacter(int characterId);
        Quote AddQuote(Quote quote);
        void Save();
    }
}