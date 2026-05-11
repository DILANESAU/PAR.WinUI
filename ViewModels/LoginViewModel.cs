using CommunityToolkit.Mvvm.ComponentModel;
using PAR.Core.Models;
using CommunityToolkit.Mvvm.Input;
using PAR.WinUI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PAR.Core.Helpers;

namespace PAR.WinUI.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly ApiAuthService _apiAuth;

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set { SetProperty(ref _username, value); ErrorMessage = string.Empty; }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set { SetProperty(ref _password, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { SetProperty(ref _isBusy, value); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { SetProperty(ref _errorMessage, value); }
        }

        public AsyncRelayCommand LoginCommand { get; }

        public LoginViewModel()
        {
            _apiAuth = new ApiAuthService();
            LoginCommand = new AsyncRelayCommand(EjecutarLogin);
        }

        private async Task EjecutarLogin()
        {
            if (IsBusy) return;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Ingresa usuario y contraseña.";
                return;
            }

            IsBusy = true;
            ErrorMessage = string.Empty;
            await Task.Delay(800);

            var usuarioEncontrado = await _apiAuth.HacerLoginAsync(Username, Password);
            IsBusy = false;

            if (usuarioEncontrado != null)
            {
                if (usuarioEncontrado.RequiereCambioPwd)
                {
                    ErrorMessage = "Requiere cambio de contraseña (Falta implementar vista de cambio).";
                }
                else
                {
                    EntrarAlSistema(usuarioEncontrado);
                }
            }
            else
            {
                ErrorMessage = "Credenciales incorrectas o error de conexión.";
            }
        }

        private void EntrarAlSistema(UsuarioModel usuarioValido)
        {
            Session.UsuarioActual = usuarioValido;
        }
    }
}
