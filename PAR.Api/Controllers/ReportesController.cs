using Microsoft.AspNetCore.Mvc;
using PAR.Api.Services;
using PAR.Core.Models;
namespace PAR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    { 
        private readonly ReportesService _reportesService;

        public ReportesController(ReportesService reportesService)
        {
            _reportesService = reportesService;
        }
        [HttpGet("ventas")]
        public async Task<ActionResult<List<VentaReporteModel>>> ObtenerVentas(int sucursalId, DateTime inicio, DateTime fin)
        {
            try
            {
                var ventas = await _reportesService.ObtenerVentasRangoAsync(sucursalId, inicio, fin);

                if (ventas == null) return NotFound("No se encontraron datos.");

                return Ok(ventas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("tendencia")]
        public async Task<ActionResult<List<GraficoPuntoModel>>> ObtenerTendencia(int sucursalId, string periodo)
        {
            try
            {
                var tendencia = await _reportesService.ObtenerTendenciaGrafica(sucursalId, periodo);

                if (tendencia == null) return NotFound("No hay datos de tendencia.");

                return Ok(tendencia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
