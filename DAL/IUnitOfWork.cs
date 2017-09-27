using DAL.Model;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IUnitOfWork
    {

        IGenericRepository<Movie> MovieRepository { get; }

        ICharacterRepository CharacterRepository { get; }

        IQuoteRepository QuoteRepository { get; }

        void Save();

    }
}
