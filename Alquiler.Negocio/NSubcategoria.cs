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
    public class NSubcategoria
    {
        public static DataTable Listar()
        {
            DSubcategoria Datos = new DSubcategoria();
            return Datos.Listar();
        }

        public static DataTable Buscar(string Valor)
        {
            DSubcategoria Datos = new DSubcategoria();
            return Datos.Buscar(Valor);
        }
        

        public static string Insertar(string Nombre,int IdCategoria)
        {
            DSubcategoria Datos = new DSubcategoria();

            string Existe = Datos.Existe(Nombre,IdCategoria);
            if (Existe.Equals("1"))
            {
                return "La subcategoría ya existe";
           }
            else
            {
                Subcategoria Obj = new Subcategoria();
                Obj.Nombre = Nombre;
                
                Obj.IdCategoria = IdCategoria;

                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int Id, int IdCategoria , string NombreAnt, string Nombre)
        {
            DSubcategoria Datos = new DSubcategoria();
                Subcategoria Obj = new Subcategoria();


                string Existe = Datos.Existe(Nombre,IdCategoria);
                if (Existe.Equals("1"))
                {
                    return "La subcategoría ya existe";
                }
                else
                {
                    
                    Obj.IdSubcategoria = Id;
                    Obj.Nombre = Nombre;
                    Obj.IdCategoria = IdCategoria;

                    return Datos.Actualizar(Obj);
                }
            

        }

        public static string Eliminar(int Id)
        {
            DSubcategoria Datos = new DSubcategoria();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DSubcategoria Datos = new DSubcategoria();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DSubcategoria Datos = new DSubcategoria();
            return Datos.Desactivar(Id);
        }
    }
}
