using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.CORE.DTOs.Reporte
{
    public class DatosConsultaDto
    {
        public string Usuario { get; set; } = string.Empty;
        public string Lunes { get; set; } = string.Empty;
        public string Martes { get; set; } = string.Empty;
        public string Miércoles { get; set; } = string.Empty;
        public string Jueves { get; set; } = string.Empty;
        public string Viernes { get; set; } = string.Empty;
        public string Sábado { get; set; } = string.Empty;
        public string Domingo { get; set; } = string.Empty;
    }
}
