using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquiler.Entidades
{
    public class Alquiler
    {
        public int IdAlquiler { get; set; }
        public int IdPersona { get; set; }
        public DateTime Fecha_Alquiler { get; set; }

        public DateTime Fecha_Devolucion { get; set; }

        public DateTime Fecha_Entregado { get; set; }

        public decimal Monto { get; set; }

        public string Estado { get; set; }

        public DataTable Detalle { get; set; }
    }
}
