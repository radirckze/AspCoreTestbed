using DAL.Model;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;


namespace DAL.Repositories 
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
            try
            {
                return context.Quote.Where(q => q.MovieId == movieId)
                    .Include(q => q.AppearsIn).ThenInclude(m => m.Movie)
                    .Include(q => q.AppearsIn).ThenInclude(c => c.Character).ToList();

            }
            catch (Exception ex)
            {
                //log error
                throw ex;
            }
        }

        // using raw SQL statement instead of the EF LINQ
        public IEnumerable<Quote> GetQuotesByMovieCustom(int movieId)
        {
            try
            {
                string sqlStatement = @"SELECT movie.name, mc.name, quote.quote FROM dbo.quote 
                    inner join movie on movie.id = quote.movie_id
                    inner join mcharacter as mc on mc.id = quote.character_id 
                    where quote.movie_id = " + movieId + ";";

                // In EF core, need to drop down to ADO level to query directly from DB
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sqlStatement;
                    context.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        // convert each record to a custom class and return list of ...
                    }
                }

                throw new NotImplementedException("Quote Repository GetQuotesByMovieCustom");
            }
            catch (Exception ex)
            {
                //log error
                throw ex;
            }
        }

        public IEnumerable<Quote> GetQuotesByCharacter(int characterId) {

            return context.Quote.Where(q => q.CharacterId == characterId)
                    .Include(q => q.AppearsIn).ThenInclude(m => m.Movie)
                    .Include(q => q.AppearsIn).ThenInclude(c => c.Character).ToList();

        }

        public  Quote AddQuote(Quote quote)
        {
            try
            {
                context.Quote.Add(quote);
                return quote;
            }
            catch (Exception ex)
            {
                //do someting
                throw ex;
            }
        }

        public void DeleteQuote(int quoteId)
        {
            try
            {
                Quote quote = context.Quote.Find(quoteId);
                if (quote != null)
                {
                    context.Quote.Remove(quote);
                }
            }
            catch (Exception ex)
            {
                //do someting
                throw ex;
            }

        }

        //public void Save() {

        //    throw new NotImplementedException();
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}