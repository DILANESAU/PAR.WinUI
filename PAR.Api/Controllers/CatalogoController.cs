using Microsoft.AspNetCore.Mvc;
using PAR.Api.Services;

namespace PAR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoController : ControllerBase
    {
        private readonly CatalogoDataService _catalogoData;

        public CatalogoController(CatalogoDataService catalogoData)
        {
            _catalogoData = catalogoData;
        }
        [HttpGet("obtener")]
        public async Task<IActionResult> ObtenerCatalogo()
        {
            try
            {
                var catalogo = await _catalogoData.ObtenerCatalogoSqlAsync();
                return Ok(catalogo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR CRÍTICO AL OBTENER CATÁLOGO: " + ex.Message);
                return StatusCode(500, "Error al obtener el catálogo");
            }
        }
    }
}
