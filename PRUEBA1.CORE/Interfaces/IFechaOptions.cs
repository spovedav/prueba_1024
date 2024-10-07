using PRUEBA1.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA1.CORE.Interfaces
{
    public interface IFechaOptions
    {
        (DateTime inicioSemana, DateTime finSemana) ObtenerRangoSemana(DateTime fecha);
        int ObtenerNumeroSemana(DateTime fecha);
        Dictionary<string, RangoFechasDto> ObtenerDiasSeman(DateTime fechaSeleccionada);
        DateTime ObtenerFechaXdia(Dictionary<string, DateTime> fechas, string dia);
    }
}
