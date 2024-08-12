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
    public class NMarca
    {
        public static DataTable Listar()
        {
            DMarca Datos = new DMarca();
            return Datos.Listar();
        }

        public static DataTable Buscar(string Valor)
        {
            DMarca Datos = new DMarca();
            return Datos.Buscar(Valor);
        }

        public static string Insertar(string Nombre)
        {
            DMarca Datos = new DMarca();

            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "La marca ya existe";
            }
            else
            {
                Marca Obj = new Marca();
                Obj.Nombre = Nombre;

                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int Id, string NombreAnt, string Nombre)
        {
            DMarca Datos = new DMarca();
            Marca Obj = new Marca();

            if (NombreAnt.Equals(Nombre))
            {
                Obj.IdMarca = Id;
                Obj.Nombre = Nombre;

                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "La marca ya existe";
                }
                else
                {

                    Obj.IdMarca = Id;
                    Obj.Nombre = Nombre;

                    return Datos.Actualizar(Obj);
                }
            }

        }

        public static DataTable Seleccionar()
        {
            DMarca Datos = new DMarca();
            return Datos.Seleccionar();
        }

        public static string Eliminar(int Id)
        {
            DMarca Datos = new DMarca();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DMarca Datos = new DMarca();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DMarca Datos = new DMarca();
            return Datos.Desactivar(Id);
        }
    }
}
