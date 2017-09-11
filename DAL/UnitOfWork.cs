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
        private IMovieRepository movieRepository = null;
        private IGenericRepository<Character> characterRepository = null;
        private bool disposed = false;

        public IMovieRepository MovieRepository
        {
            get
            {
                if (movieRepository == null)
                {
                    movieRepository = new MovieRepository(mbContext);
                }

                return movieRepository;
            }
        }

        public IGenericRepository<Character> CharacterRepository
        {
            get
            {
                if (characterRepository == null)
                {
                    characterRepository = new GenericRepository<Character>(mbContext);
                }

                return characterRepository;
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