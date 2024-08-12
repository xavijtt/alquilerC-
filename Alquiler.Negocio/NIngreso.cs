using Alquiler.Datos;
using Alquiler.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquiler.Negocio
{
    public class NIngreso
    {
        public static DataTable Listar()
        {
            DIngreso Datos = new DIngreso();
            return Datos.Listar();
        }

        public static DataTable Buscar(DateTime fechainicial, DateTime fechafinal)
        {
            DIngreso Datos = new DIngreso();
            return Datos.Buscar(fechainicial,fechafinal);
        }

        public static DataTable ListarDetalle(int Id)
        {
            DIngreso Datos = new DIngreso();
            return Datos.ListarDetalle(Id);
        }

        public static string Insertar(int IdUsuario, decimal Total, DataTable Detalle)
        {
            DIngreso Datos = new DIngreso();
            Ingreso Obj = new Ingreso();
            
            Obj.IdUsuario = IdUsuario; 
            Obj.Total = Total;
            Obj.Detalle = Detalle;
            return Datos.Insertar(Obj);
        }
        public static string Anular(int Id)
        {
            DIngreso Datos = new DIngreso();
            return Datos.Anular(Id);
        }
    }
}
