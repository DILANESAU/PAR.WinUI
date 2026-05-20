using Newtonsoft.Json;
using PAR.Core.Models;

namespace PAR.Api.Services
{
    public class ClientesService
    {
        private readonly SqlHelper _sqlHelper;

        public ClientesService(string connectionString)
        {
            _sqlHelper = new SqlHelper(connectionString);
        }
        public async Task<List<ClienteAnalisisModel>> ObtenerDatosBase(int anioActual, int sucursalId)
        {
            string query = @"
                SELECT JsonDatosBase 
                FROM Cache_Clientes 
                WHERE IdSucursal = @Sucursal AND Anio = @Anio";

            var json = await _sqlHelper.QueryFirstOrDefaultAsync<string>(query, new { Sucursal = sucursalId, Anio = anioActual });

            return string.IsNullOrEmpty(json)
                ? new List<ClienteAnalisisModel>()
                : JsonConvert.DeserializeObject<List<ClienteAnalisisModel>>(json);
        }

        public async Task<KpiClienteModel> ObtenerKpisCliente(string claveCliente, int anio, int sucursalId)
        {
            string query = @"
                SELECT JsonKpi 
                FROM Cache_Clientes_Detalle 
                WHERE IdSucursal = @Sucursal AND Anio = @Anio AND ClaveCliente = @Cliente";

            var parametros = new { Sucursal = sucursalId, Anio = anio, Cliente = claveCliente };

            var json = await _sqlHelper.QueryFirstOrDefaultAsync<string>(query, parametros);

            return string.IsNullOrEmpty(json)
                ? new KpiClienteModel()
                : JsonConvert.DeserializeObject<KpiClienteModel>(json);
        }
        public async Task<List<ProductoAnalisisModel>> ObtenerVariacionProductos(string claveCliente, int anioActual, int sucursalId)
        {
            string query = @"
                SELECT JsonVariacion 
                FROM Cache_Clientes_Detalle 
                WHERE IdSucursal = @Sucursal AND Anio = @Anio AND ClaveCliente = @Cliente";

            var parametros = new { Sucursal = sucursalId, Anio = anioActual, Cliente = claveCliente };

            var json = await _sqlHelper.QueryFirstOrDefaultAsync<string>(query, parametros);

            return string.IsNullOrEmpty(json)
                ? new List<ProductoAnalisisModel>()
                : JsonConvert.DeserializeObject<List<ProductoAnalisisModel>>(json);
        }
        public async Task<List<HistoricoClienteModel>> ObtenerHistorialCliente(string claveCliente, int sucursalId)
        {
            string query = @"
                SELECT JsonHistorico 
                FROM Cache_Clientes_Detalle 
                WHERE IdSucursal = @Sucursal AND ClaveCliente = @Cliente";

            var parametros = new { Sucursal = sucursalId, Cliente = claveCliente };

            var json = await _sqlHelper.QueryFirstOrDefaultAsync<string>(query, parametros);

            return string.IsNullOrEmpty(json)
                ? new List<HistoricoClienteModel>()
                : JsonConvert.DeserializeObject<List<HistoricoClienteModel>>(json);
        }
    }
}