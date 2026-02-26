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
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<HospitalDbContext>(options =>
                    options.UseSqlServer(
                    context.Configuration.GetConnectionString("DefaultConnection")));
                services.AddScoped<IPatientRepository, PatientRepository>();
                services.AddScoped<IDoctorRepository, DoctorRepository>();
                services.AddScoped<IHistoryRepository, HistoryRepository>();
                services.AddScoped<IMedicineRepository, MedicineRepository>();
                services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
                services.AddScoped<IPatientService, PatientService>();
                services.AddScoped<IDoctorService, DoctorService>();
                services.AddScoped<IHistoryService, HistoryService>();
                services.AddScoped<IMedicineService, MedicineService>();
                services.AddScoped<IPrescriptionService, PrescriptionService>();
                services.AddTransient<MainWindow>();
            })
            .Build();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using var scope = _host.Services.CreateScope();
            var mainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }
    }

}
