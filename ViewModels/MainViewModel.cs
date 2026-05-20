using PAR.WinUI.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

namespace PAR.WinUI.ViewModels
{
    public class MainViewModel
    {
        private readonly INavigationService _navigationService;

        // Inyectamos el servicio de navegación
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public void Navegar(string vista)
        {
            if ( vista == "Configuracion" )
            {
                // _navigationService.NavigateTo("Configuracion");
            }
            else
            {
                _navigationService.NavigateTo(vista);
            }
        }

        public void CargarInicio()
        {
            _navigationService.NavigateTo("Dashboard");
        }
    }
}
