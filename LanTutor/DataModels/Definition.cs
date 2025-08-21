namespace LanTutor.DataModels
{
    public class Definition
    {
        public int DefinitionId { get; set; }
        public int WordId { get; set; }
        public string Description { get; set; }

        public WordTransDef Word { get; set; }
    }


}
