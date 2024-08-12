using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquiler.Entidades
{
    public class Conjunto
    {
        public int IConjunto { get; set; }
       
        public string Nombre { get;set; }

        public DataTable Detalle { get; set; }
    }
}
