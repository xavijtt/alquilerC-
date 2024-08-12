using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Alquiler.Datos
{
    public class Conexion
    {
        public static Conexion Con = null;

        public SqlConnection CrearConexion()
        { 
        
            SqlConnection Cadena = new SqlConnection();

            try
            {
                Cadena.ConnectionString = "Data Source=DESKTOP-VEFB15T;Initial Catalog=db_alquiler;User ID=sa;Password=javitc78445751;Encrypt=False";
            }
            catch (Exception ex)
            {

                Cadena = null;
                throw ex;
            }

            return Cadena;
        }

        public static Conexion getInstancia()
        {
            if (Con==null)
            {
                Con = new Conexion();
            }
            return Con;
        }
    }
}
