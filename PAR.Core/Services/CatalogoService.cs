using System;
using System.Collections.Generic;
using PAR.Core.Models;
using PAR.Core.Services.Interfaces;

namespace PAR.Core.Services
{
    public class CatalogoService
    {
        private Dictionary<string, ProductoInfo> _catalogo;
        private readonly IBusinessLogicService _businessLogic;

        public CatalogoService(IBusinessLogicService businessLogic)
        {
            _businessLogic = businessLogic;
            _catalogo = new Dictionary<string, ProductoInfo>(StringComparer.OrdinalIgnoreCase);
        }
        public void CargarDatos(IEnumerable<ProductoInfo> listaProductos)
        {
            _catalogo.Clear();

            if (listaProductos == null) return;

            foreach (var item in listaProductos)
            {
                item.Familia = _businessLogic.NormalizarFamilia(item.Familia ?? "Otros");

                item.CodigoArticulo = item.CodigoArticulo?.Trim() ?? "";
                item.Descripcion = item.Descripcion?.Trim() ?? "Sin Descripción";

                if (!string.IsNullOrEmpty(item.CodigoArticulo) && !_catalogo.ContainsKey(item.CodigoArticulo))
                {
                    _catalogo.Add(item.CodigoArticulo, item);
                }
            }
        }
        public ProductoInfo ObtenerInfo(string codigoProducto)
        {
            if (!string.IsNullOrWhiteSpace(codigoProducto) && _catalogo.ContainsKey(codigoProducto))
            {
                return _catalogo[codigoProducto];
            }
            return new ProductoInfo
            {
                CodigoArticulo = codigoProducto ?? "Desconocido",
                Descripcion = "Producto (Sin Catálogo)",
                Categoria = "Sin Categoria",
                Grupo = "Sin Grupo",
                Familia = "Otros",
                Linea = "Sin Linea",
                ColorTipo = "Sin Color",
                Litros = 0m
            };
        }
    }
}