using LanTutor.DataModels;
using System.ComponentModel.DataAnnotations;

namespace LanTutor.DataModels
{
    public class WordScore : ScoreParameters
    {
        [Key]
        public int WordScoreId
        {
            get;
            set;
        }

        public int WordTransDefId
        {
            get;
            set;
        }

        public WordTransDef WordTransDef
        {
            get;
            set;
        }
    }
}