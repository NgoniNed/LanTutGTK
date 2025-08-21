using Gtk;

namespace LanTutor
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            using (var context = new LanTutor.Database.LanTutorContext())
            {
                context.Database.EnsureCreated();
                context.SeedData();
            }

            new LTGUIDesign("LanTutor 1.7");
            Application.Run();
            
        }
        
    }
}
