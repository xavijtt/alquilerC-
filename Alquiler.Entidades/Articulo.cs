using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquiler.Entidades
{
    public class Articulo
    {
        public int IdArticulo { get; set; }

        public int IdSubCategoria { get; set; }
        public int IdMarca { get; set; }

        public string Modelo { get; set; }

        public int Stock { get; set; }

        public int StockDisp { get; set; }
        
        public bool Estado { get; set; }
    }
}
