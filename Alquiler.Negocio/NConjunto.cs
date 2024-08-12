using Alquiler.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquiler.Negocio
{
    public class NConjunto
    {

        public static DataTable Listar()
        {
            DConjunto Datos = new DConjunto();
            return Datos.Listar();
        }
        public static DataTable ListarDetalle(int Id)
        {
            DConjunto Datos = new DConjunto();
            return Datos.ListarDetalle(Id);
        }

        

        public static string Insertar(string Nombre, DataTable Detalle)
        {
            DConjunto Datos = new DConjunto();
            Alquiler.Entidades.Conjunto Obj = new Alquiler.Entidades.Conjunto();

          
            Obj.Nombre = Nombre;
            Obj.Detalle = Detalle;
            return Datos.Insertar(Obj);
        }

        

        public static DataTable Buscar(string Valor)
        {
            DConjunto Datos = new DConjunto();
            return Datos.Buscar(Valor);
        }

        public static string Eliminar(int Id)
        {
            DConjunto Datos = new DConjunto();
            return Datos.Eliminar(Id);
        }



    }
}
