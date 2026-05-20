using PAR.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PAR.WinUI.Services
{
    public class ReportesService
    {
        private readonly HttpClient _httpClient;

        public ReportesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VentaReporteModel>> ObtenerVentasDetalleAsync(int sucursalId, DateTime inicio, DateTime fin)
        {
            try
            {
                string url = $"api/reportes/ventas?sucursalId={sucursalId}&inicio={inicio:yyyy-MM-dd}&fin={fin:yyyy-MM-dd}";
                var response = await _httpClient.GetFromJsonAsync<List<VentaReporteModel>>(url);
                return response ?? new List<VentaReporteModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo API: {ex.Message}");
                return new List<VentaReporteModel>();
            }
        }

        public async Task<List<GraficoPuntoModel>> ObtenerTendenciaGrafica(int sucursalId, string periodo)
        {
            try
            {
                string url = $"api/reportes/tendencia?sucursalId={sucursalId}&periodo={Uri.EscapeDataString(periodo)}";
                var response = await _httpClient.GetFromJsonAsync<List<GraficoPuntoModel>>(url);
                return response ?? new List<GraficoPuntoModel>();
            }
            catch (Exception)
            {
                return new List<GraficoPuntoModel>();
            }
        }
    }
}
