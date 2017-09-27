using DAL.Interfaces;
using DAL.Model;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {

        private MovieBuffContext context = null;
        private bool disposed = false;

        public CharacterRepository(MovieBuffContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("missing context");
            }

            this.context = context;
        }

        public IEnumerable<Mcharacter> GetCharacters()
        {
            try
            {
                return context.Mcharacter.ToList();
            }
            catch (Exception ex)
            {
                //log error
                throw ex;
            }
        }

        public Mcharacter GetCharacterByName(string characterName)
        {
            try
            {
                return context.Mcharacter.FirstOrDefault(a => a.Name == characterName);
            }
            catch (Exception ex)
            {
                // do someting
                throw ex;
            }
        }

        public Mcharacter AddCharacter(Mcharacter character)
        {
            try
            {
                context.Mcharacter.Add(character);
                return character;
            }
            catch (Exception ex)
            {
                //do someting
                throw ex;
            }
        }

        public AppearsIn AddCharacteAppearsIn(int characterId, int movieId)
        {
            try
            {
                AppearsIn appearsIn = new AppearsIn()
                    { CharacterId = characterId, MovieId = movieId };
                context.AppearsIn.Add(appearsIn);
                return appearsIn;
            }
            catch (Exception ex)
            {
                //do someting
                throw ex;
            }
        }

        public void UpdateCharacter(Mcharacter character)
        {
            context.Entry(character).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void DeleteCharacter(int characterId)
        {
            Mcharacter character = context.Mcharacter.Find(characterId);
            if (character != null)
            {
                context.Mcharacter.Remove(character);
            }
        }

        public void DeleteCharacteAppearsIn(int characterId, int movieId)
        {
            try
            {
                AppearsIn appearsIn = context.AppearsIn.Find(movieId, characterId);
                if (appearsIn != null)
                {
                    context.AppearsIn.Remove(appearsIn);
                }
            }
            catch (Exception ex)
            {
                //do someting
                throw ex;
            }
        }

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
