using Gtk;

namespace LanTutor
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            new LTGUIDesign();
            Application.Run();
            
        }
        
    }
}
