using Gtk;

namespace LanTutor
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            new LTGUIDesign("LanTutor 1.0");
            Application.Run();
            
        }
        
    }
}
