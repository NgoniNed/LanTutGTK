using System.ComponentModel.DataAnnotations;

namespace LanTutor.DataModels
{
    public class DescriptionScore : ScoreParameters
    {
        [Key]
        public int DescriptionScoreId
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