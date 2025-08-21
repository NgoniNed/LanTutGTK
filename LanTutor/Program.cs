using Gtk;
using LanTutor.Adapters;
using LanTutor.Database;
using LanTutor.Interfaces;
using LanTutor.Services;
using Microsoft.EntityFrameworkCore;
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
            services.AddScoped<ScoreService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<ISessionService, SessionService>();

            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddScoped<XmlAdapter>();

            services.AddSingleton<ILanTutorFrontend, AutoAdapter>();

            var serviceProvider = services.BuildServiceProvider();
            
            using (var context = new LanTutor.Database.LanTutorContext())
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();
                context.SeedData();
            }
            var autoAdapter = serviceProvider.GetService<ILanTutorFrontend>();

            new LTGUIDesign(autoAdapter,"LanTutor 1.15");
            Application.Run();
            
        }
        
    }
}
