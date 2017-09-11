using DAL.Model;

using System;
using System.Collections.Generic;

namespace DAL.Interface 
{
    public interface ICharacterRepository
    {
        IEnumerable<Character> GetCharacters();
        Character AddCharacter(Character character);
        //void DeleteCharacter(int characterId);
        void AddAppearsIn(int characterId, int movieId);
        void Save();
    }
}