
using LanTutor.DataModels;
using System.Collections.Generic;

namespace LanTutor.Services
{
    public interface IWordService
    {
        List<WordTransDef> GetAllWords();
        WordTransDef GetWordById(int wordId);
        void AddWord(WordTransDef word);
        void AddDefinition(int wordId, string description);
        List<Definition> GetDefinitionsForWord(int wordId);
    }

}