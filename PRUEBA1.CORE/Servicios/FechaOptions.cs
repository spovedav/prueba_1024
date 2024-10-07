using PRUEBA1.CORE.DTOs;
using PRUEBA1.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA1.CORE.Servicios
{
    public class FechaOptions : IFechaOptions
    {
        public Dictionary<string, RangoFechasDto> ObtenerDiasSeman(DateTime fechaSeleccionada)
        {
            (DateTime inicioSemana, DateTime finSemana) = ObtenerRangoSemana(fechaSeleccionada);

            Dictionary<string, RangoFechasDto> DíasSemana = new Dictionary<string, RangoFechasDto>();
            DíasSemana.Add("Usuario", new RangoFechasDto() { fecha = null, formato = "Usuario" });

            for (DateTime fecha = inicioSemana; fecha <= finSemana; fecha = fecha.AddDays(1))
            {
                string diaMes = fecha.ToString("dd");
                string diaSemana = fecha.ToString("dddd");
                DíasSemana.Add(diaSemana, new RangoFechasDto() { fecha = fecha, formato = $"{diaSemana}({diaMes})" });
            }

            return DíasSemana;
        }

        public DateTime ObtenerFechaXdia(Dictionary<string, DateTime> fechas, string dia)
        {
            if (fechas.TryGetValue(dia, out DateTime fecha))
            {
                Console.WriteLine($"La fecha de {dia} es: {fecha.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine($"La clave '{dia}' no existe en el diccionario.");
            }

            return fecha;
        }

        public int ObtenerNumeroSemana(DateTime fecha)
        {
            CultureInfo cultura = CultureInfo.InvariantCulture;

            Calendar calendario = cultura.Calendar;

            CalendarWeekRule reglaSemana = cultura.DateTimeFormat.CalendarWeekRule;
            DayOfWeek primerDiaSemana = cultura.DateTimeFormat.FirstDayOfWeek;

            return calendario.GetWeekOfYear(fecha, reglaSemana, primerDiaSemana);
        }

        public (DateTime inicioSemana, DateTime finSemana) ObtenerRangoSemana(DateTime fecha)
        {
            DayOfWeek diaSemana = fecha.DayOfWeek;

            int diferenciaConLunes = ((int)diaSemana - (int)DayOfWeek.Monday + 7) % 7;

            DateTime inicioSemana = fecha.AddDays(-diferenciaConLunes);

            DateTime finSemana = inicioSemana.AddDays(6);

            return (inicioSemana, finSemana);
        }
    }
}
