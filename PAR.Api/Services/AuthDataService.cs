using Microsoft.AspNetCore.Identity;
using PAR.Core.Models;
using PAR.Core.Services; // Asegúrate de que SqlHelper esté aquí
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PAR.Api.Services
{
    public class AuthDataService
    {
        private readonly string _connectionString;

        public AuthDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UsuarioModel> ValidarLoginAsync(string usuarioInput, string passwordInput)
        {
            var sqlHelper = new SqlHelper(_connectionString);

            string query = @"
                SELECT 
                    u.Id AS IdUsuario,
                    u.Username, 
                    u.PasswordHash,
                    u.NombreCompleto, 
                    u.RolId,
                    r.Nombre AS Rol,
                    u.RequiereCambioPwd 
                FROM Usuarios u
                INNER JOIN Roles r ON u.RolId = r.Id
                WHERE u.Username = @User AND u.Activo = 1";

            var parametros = new { User = usuarioInput };
            var usuarios = await sqlHelper.QueryAsync<UsuarioModel>(query, parametros);
            var usuarioEncontrado = usuarios.FirstOrDefault();

            if (usuarioEncontrado == null) return null;

            bool esValido = PasswordHasher.VerifyPassword(passwordInput, usuarioEncontrado.PasswordHash);
            if (!esValido) return null;

            if (usuarioEncontrado.Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase) ||
                usuarioEncontrado.Rol.Equals("Director", StringComparison.OrdinalIgnoreCase) ||
                usuarioEncontrado.Rol.Equals("Sistemas", StringComparison.OrdinalIgnoreCase))
            {
                usuarioEncontrado.SucursalesPermitidas = null;
            }
            else
            {
                string queryPermisos = "SELECT SucursalId FROM UsuarioSucursales WHERE IdUsuario = @Id";
                var listaIds = await sqlHelper.QueryAsync<int>(queryPermisos, new { Id = usuarioEncontrado.IdUsuario });
                usuarioEncontrado.SucursalesPermitidas = listaIds.ToList();
            }

            return usuarioEncontrado;
        }
    }
}