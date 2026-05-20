using Microsoft.AspNetCore.Mvc;
using PAR.Api.Services;

namespace PAR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthDataService _authData;

        public AuthController(AuthDataService authData)
        {
            _authData = authData;
        }

        public class LoginRequest
        {
            public string Usuario { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _authData.ValidarLoginAsync(request.Usuario, request.Password);
            if (usuario == null)
            {
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
            }
            return Ok(usuario);
        }
    }
}
