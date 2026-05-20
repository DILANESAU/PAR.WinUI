using System;
using System.Collections.Generic;
using System.Text;
using PAR.Core.Models;

namespace PAR.Core.Helpers
{
    public static class Session
    {
        public static UsuarioModel UsuarioActual { get; set; }
        public static void Logout()
        {
            UsuarioActual = null;
        }
    }
}
