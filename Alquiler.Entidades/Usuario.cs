using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquiler.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public int IdRol { get; set; }
        public string User { get; set; }

        public string Clave { get; set; }
        public bool Estado { get; set; }
    }
}
