using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using PAR.Core.Services;
using PAR.WinUI.Interfaces;
using PAR.WinUI.Services;
using PAR.WinUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace PAR.WinUI
{
    public partial class App : Application
    {
        private Window? _window;

        public IServiceProvider Services { get; }

        public App()
        {
            InitializeComponent();
            Services = ConfigureServices();
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // --- A. SERVICIOS DE CONEXIÓN (API) ---
            services.AddSingleton(new HttpClient { BaseAddress = new Uri("http://localhost:5198") });
            services.AddSingleton<ReportesService>();
            services.AddSingleton<ApiAuthService>(); 

            // --- B. SERVICIOS DE UI ---
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // --- C. SERVICIOS LÓGICOS ---
            services.AddSingleton<SucursalesService>();
            services.AddSingleton<FilterService>();

            // --- D. VIEWMODELS ---
            services.AddSingleton<MainViewModel>();
            services.AddTransient<DashboardViewModel>();


            return services.BuildServiceProvider();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new Views.LoginWindow();
            _window.Activate();
        }
    }
}
