using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquiler.Entidades
{
    public class Subcategoria
    {
        public int IdSubcategoria { get; set; }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
