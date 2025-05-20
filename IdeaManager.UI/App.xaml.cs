using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using IdeaManager.Data;
using IdeaManager.Data.Db;
using Microsoft.EntityFrameworkCore;
using IdeaManager.Core.Interfaces;
using IdeaManager.Services.Services;
using IdeaManager.UI.Views;
using IdeaManager.UI.ViewModels;

namespace IdeaManager.UI
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddDataServices("Data Source=ideas.db");
            services.AddScoped<IIdeaService, IdeaService>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<DashboardView>();
            services.AddTransient<IdeaFormView>();
            services.AddTransient<IdeaListView>();

            services.AddTransient<DashboardViewModel>();
            services.AddTransient<IdeaFormViewModel>();
            services.AddTransient<IdeaListViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ideas.db");
            if (File.Exists(dbPath))
                File.Delete(dbPath);


            using (var scope = ServiceProvider.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<IdeaDbContext>();
                ctx.Database.EnsureCreated();
            }

            var mw = ServiceProvider.GetRequiredService<MainWindow>();
            mw.Show();
        }
    }
}
