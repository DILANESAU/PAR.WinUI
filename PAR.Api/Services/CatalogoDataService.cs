using PAR.Core.Models;
using PAR.Core.Services.Interfaces;

namespace PAR.Api.Services
{
    public class CatalogoDataService
    {
        private readonly SqlHelper _sql;
        private readonly IBusinessLogicService _businessLogic;

        public CatalogoDataService(string connectionString, IBusinessLogicService businessLogic)
        {
            _sql = new SqlHelper(connectionString);
            _businessLogic = businessLogic;
        }

        public async Task<List<ProductoInfo>> ObtenerCatalogoSqlAsync()
        {
            try
            {
                string query = @"
                    SELECT 
                        CodigoArticulo, 
                        Descripcion, 
                        Categoria,
                        Grupo,
                        Familia, 
                        ISNULL([A.Linea], 'Sin Linea') AS Linea, 
                        ISNULL([Color-Tipo], 'Sin Color') AS ColorTipo, 
                        CAST(ISNULL(Litros, 0) AS DECIMAL(18,4)) AS Litros 
                    FROM Catalogo_Productos";

                var listaSql = await _sql.QueryAsync<ProductoInfo>(query);
                var listaLimpia = new List<ProductoInfo>();

                if (listaSql == null) return listaLimpia;

                foreach (var item in listaSql)
                {
                    item.Familia = _businessLogic.NormalizarFamilia(item.Familia ?? "Otros");

                    item.CodigoArticulo = item.CodigoArticulo?.Trim() ?? "";
                    item.Descripcion = item.Descripcion?.Trim() ?? "Sin Descripción";

                    listaLimpia.Add(item);
                }

                return listaLimpia;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR CRÍTICO SQL CATÁLOGO: " + ex.Message);
                return new List<ProductoInfo>();
            }
        }
    }
}
