using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using PAR.WinUI.Interfaces;
using PAR.WinUI.ViewModels;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;

namespace PAR.WinUI
{
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }
        public MainWindow()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;

            var app = ( App ) Application.Current;
            ViewModel = app.Services.GetRequiredService<MainViewModel>();
            var navService = app.Services.GetRequiredService<INavigationService>();

            navService.Initialize(ContentFrame);
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.CargarInicio();
            NavView.SelectedItem = NavView.MenuItems[0];
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string tag = args.IsSettingsInvoked ? "Configuracion" : args.InvokedItemContainer.Tag.ToString();

            ViewModel.Navegar(tag);
        }
    }
}
