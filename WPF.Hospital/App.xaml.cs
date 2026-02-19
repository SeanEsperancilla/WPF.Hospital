using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using WPF.Hospital.Repository;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;

        protected override void OnStartup(StartupEventArgs e)
        { 
            base.OnStartup(e);
            _host = new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<HospitalDbContext>(options => {
                        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"));
                    });

                    services.AddScoped<IPatientRepository, PatientRepository>();
                    services.AddScoped<IHistoryRepository, HistoryRepository>();

                    services.AddScoped<IPatientService, PatientService>();

                    services.AddTransient<MainWindow>();
                })
                .Build();
            _host.Start();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        }
    }

}
