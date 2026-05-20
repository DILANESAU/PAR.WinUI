using System;
using System.Collections.Generic;
using System.Text;

namespace PAR.Core.Services.Interfaces
{
    public interface IBusinessLogicService
    {
        string NormalizarFamilia(string textoRaw);
        string ObtenerColorFamilia(string nombreFamilia);
    }
}
