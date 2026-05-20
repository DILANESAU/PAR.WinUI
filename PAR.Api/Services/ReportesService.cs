using Newtonsoft.Json;
using PAR.Core.Models;

namespace PAR.Api.Services
{
    public class ReportesService
    {
        private readonly SqlHelper _sqlHelper;

        public ReportesService(string connectionString)
        {
            _sqlHelper = new SqlHelper(connectionString);
        }
        public async Task<List<VentaReporteModel>?> ObtenerVentasDetalleAsync(int sucursalId)
        {
            string query = @"
                SELECT JsonVentas 
                FROM Cache_VentasDetalle 
                WHERE IdSucursal = @Sucursal AND Anio = @Anio AND Mes = @Mes";

            var parametros = new
            {
                Sucursal = sucursalId,
                Anio = DateTime.Now.Year,
                Mes = DateTime.Now.Month
            };

            var json = await _sqlHelper.QueryFirstOrDefaultAsync<string>(query, parametros);

            return string.IsNullOrEmpty(json)
                ? new List<VentaReporteModel>()
                : JsonConvert.DeserializeObject<List<VentaReporteModel>>(json);
        }

        public async Task<List<GraficoPuntoModel>> ObtenerTendenciaGrafica(int sucursalId, string periodoSeleccionado)
        {
            string query = @"SELECT JsonGraficaTendencia FROM Cache_Dashboard WHERE IdSucursal = @Sucursal";
            var json = await _sqlHelper.QueryFirstOrDefaultAsync<string>(query, new { Sucursal = sucursalId });

            if (string.IsNullOrEmpty(json)) return new List<GraficoPuntoModel>();

            var dict = JsonConvert.DeserializeObject<Dictionary<string, List<GraficoPuntoModel>>?>(json);

            if (periodoSeleccionado == "Hoy" && dict.TryGetValue("Hoy", out List<GraficoPuntoModel>? value)) return value;
            if (periodoSeleccionado == "Este Año" && dict.TryGetValue("Anio", out List<GraficoPuntoModel>? value1)) return value1;

            return dict.TryGetValue("Mes", out List<GraficoPuntoModel>? value2) ? value2 : new List<GraficoPuntoModel>();
        }
        public async Task<List<VentaReporteModel>?> ObtenerVentasRangoAsync(int sucursalId, DateTime inicio, DateTime fin)
        {
            if ( inicio.Year == DateTime.Now.Year && inicio.Month == DateTime.Now.Month && fin.Month == DateTime.Now.Month )
            {
                return await ObtenerVentasDetalleAsync(sucursalId);
            }

            string query = @"
                SELECT JsonDatos 
                FROM Cache_HistoricoAnual 
                WHERE IdSucursal = @Sucursal AND Anio = @Anio";

            var parametros = new { Sucursal = sucursalId, Anio = inicio.Year };
            var json = await _sqlHelper.QueryFirstOrDefaultAsync<string>(query, parametros);

            if ( string.IsNullOrEmpty(json) )
                return new List<VentaReporteModel>();

            var ventasAnuales = JsonConvert.DeserializeObject<List<VentaReporteModel>>(json);

            return ventasAnuales
                .Where(v => v.FechaEmision.Date >= inicio.Date && v.FechaEmision.Date <= fin.Date)
                .ToList();
        }
    }
}