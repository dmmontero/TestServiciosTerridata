using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServiciosTerridata.models
{
    public class Periodo
    {
        public string IdPeriodo { get; set; }
        public int TipoPeriodo { get; set; }
        public String Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int PeriodoPresidencial { get; set; }
        public bool Habilitado { get; set; }
        public int Orden { get; set; }
    }
}
