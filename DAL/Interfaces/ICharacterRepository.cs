using DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface ICharacterRepository
    {

        IEnumerable<Mcharacter> GetCharacters();
        Mcharacter GetCharacterByName(string characterName);
        Mcharacter AddCharacter(Mcharacter character);
        void UpdateCharacter(Mcharacter character);
        AppearsIn AddCharacteAppearsIn(int characterId, int movieId);
        void DeleteCharacteAppearsIn(int characterId, int movieId);
        void DeleteCharacter(int characterId);

    }
}
