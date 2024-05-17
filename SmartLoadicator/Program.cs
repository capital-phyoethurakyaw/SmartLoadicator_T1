using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Threading;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SmartLoadicator.Contracts.Services;
using SmartLoadicator.Contracts.Views;
using SmartLoadicator.Models;
using SmartLoadicator.Services;
using SmartLoadicator.Views;
using SmartLoadicator.TestForms;
using EL;
using SQLitePCL;

namespace SmartLoadicator
{
    internal static class Program
    {
        public static IHost _host;

        public static T GetService<T>()
            where T : class
            => _host.Services.GetService(typeof(T)) as T;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Batteries.Init();
            EL.StaticCache.SetIniInfo();
            ///string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //Register Syncfusion license https://help.syncfusion.com/common/essential-studio/licensing/how-to-generate
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCeExzWmFZfVpgcF9EYlZRR2YuP1ZhSXxXdkBjUH9fcHNXQWFbVUU=");//("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCe0xxWmFZfVpgdl9DYlZTTGYuP1ZhSXxXdkFgWX9cdXZXTmVbWUI=");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OnStartup();
            Application.Run(new Form1());
            _host.Dispose();
            _host = null;
        }
        private static async void OnStartup()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // For more information about .NET generic host see  https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
            _host = Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration(c =>
                    {
                        c.SetBasePath(appLocation);
                    })
                    .ConfigureServices(ConfigureServices)
                    .Build();

            await _host.RunAsync();
            
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // TODO WTS: Register your services, viewmodels and pages here


            // App Host
           // services.AddHostedService<ApplicationHostService>();

            // Core Services

            // Services
           // services.AddSingleton<IPageService, PageService>();
          //  services.AddSingleton<INavigationService, NavigationService>();

            // Views
           // services.AddTransient<IForm1    , Form1>();
           // services.AddTransient<IShellWindow, ShellWindow>();

            //services.AddTransient<MainPage>();

            // Configuration
            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
        }
    }
}
