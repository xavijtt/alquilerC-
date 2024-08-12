using Alquiler.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquiler.Negocio
{
    public class NRol
    {

        public static DataTable Seleccionar()
        {
            DRol Datos = new DRol();
            return Datos.Seleccionar();
        }

    }
}
