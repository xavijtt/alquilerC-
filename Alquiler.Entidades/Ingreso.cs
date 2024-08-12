using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Alquiler.Entidades
{
    public class Ingreso
    {
        public int IdIngreso { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public string Estado { get; set; }

        public DataTable Detalle { get; set; }
    }
}
