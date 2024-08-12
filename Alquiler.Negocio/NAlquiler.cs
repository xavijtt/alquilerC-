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
    public class NAlquiler
    {
        public static DataTable Listar()
        {
            DAlquiler Datos = new DAlquiler();
            return Datos.Listar();
        }
        public static DataTable ListarDetalle(int Id)
        {
            DAlquiler Datos = new DAlquiler();
            return Datos.ListarDetalle(Id);
        }

        public static string Anular(int Id)
        {
            DAlquiler Datos = new DAlquiler();
            return Datos.Anular(Id);
        }

        public static string Finalizar(int Id)
        {
            DAlquiler Datos = new DAlquiler();
            return Datos.Finalizar(Id);
        }

        public static string Insertar(int IdPersona, DateTime Fecha_Devolucion,decimal Monto, DataTable Detalle)
        {
            DAlquiler Datos = new DAlquiler();
            Alquiler.Entidades.Alquiler Obj = new Alquiler.Entidades.Alquiler();

            Obj.IdPersona = IdPersona;
            Obj.Fecha_Devolucion = Fecha_Devolucion;
            Obj.Monto = Monto ;
            Obj.Detalle = Detalle;
            return Datos.Insertar(Obj);
        }

        public static DataTable ValidarCantidad(int IdArticulo)
        {
            DAlquiler Datos = new DAlquiler();
            return Datos.ValidarCantidad(IdArticulo);
        }

        public static DataTable Buscar(string Valor)
        {
            DAlquiler Datos = new DAlquiler();
            return Datos.Buscar(Valor);
        }

        public static DataTable Buscar_Estado(string Valor)
        {
            DAlquiler Datos = new DAlquiler();
            return Datos.Buscar_Estado(Valor);
        }


        public static DataTable Buscar_Fecha(DateTime fechainicial, DateTime fechafinal)
        {
            DAlquiler Datos = new DAlquiler();
            return Datos.Buscar_Fecha(fechainicial, fechafinal);
        }
    }
}
