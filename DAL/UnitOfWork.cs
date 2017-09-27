using DAL.Interfaces;
using DAL.Repositories;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {

        private MovieBuffContext mbContext = new MovieBuffContext();
        private IGenericRepository<Movie> movieRepository = null;
        private ICharacterRepository characterRepository = null;
        private IQuoteRepository quoteRepository = null;
        private bool disposed = false;

        public ICharacterRepository CharacterRepository
        {
            get
            {
                if (characterRepository == null)
                {
                    characterRepository = new CharacterRepository(mbContext);
                }

                return characterRepository;
            }
        }

        public IGenericRepository<Movie> MovieRepository
        {
            get
            {
                if (movieRepository == null)
                {
                    movieRepository = new GenericRepository<Movie>(mbContext);
                }

                return movieRepository;
            }
        }

        public IQuoteRepository QuoteRepository
        {
            get
            {
                if (quoteRepository == null)
                {
                    quoteRepository = new QuoteRepository(mbContext);
                }

                return quoteRepository;
            }
        }

        public void Save()
        {
            mbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    mbContext.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}