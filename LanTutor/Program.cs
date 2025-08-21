using Gtk;
using LanTutor.Adapters;
using LanTutor.Database;
using LanTutor.Interfaces;
using LanTutor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LanTutor
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            var services = new ServiceCollection();
            services.AddDbContext<LanTutorContext>();
            services.AddScoped<Services.ScoreService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<ISessionService, SessionService>();

            services.AddSingleton<ILanTutorFrontend, AutoAdapter>();

            var serviceProvider = services.BuildServiceProvider();
            var autoAdapter = serviceProvider.GetService<ILanTutorFrontend>();
            /*
            using (var context = new LanTutor.Database.LanTutorContext())
            {
                context.Database.EnsureCreated();
                context.SeedData();
            }*/

            new LTGUIDesign(autoAdapter,"LanTutor 1.7");
            Application.Run();
            
        }
        
    }
}
