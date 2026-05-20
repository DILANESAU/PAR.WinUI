using System;
using System.Collections.Generic;
using System.Text;
using PAR.Core.Models;

namespace PAR.Core.Services.Interfaces
{
    public interface IClientesLogicService
    {
        List<ClienteResumenModel> ProcesarClientes(List<VentaReporteModel> ventasActuales, List<VentaReporteModel> ventasAnteriores);
    }
}
