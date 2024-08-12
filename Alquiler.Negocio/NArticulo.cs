using Alquiler.Datos;
using Alquiler.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alquiler.Negocio;

namespace Alquiler.Negocio
{
    public class NArticulo
    {
        public static DataTable Listar()
        {
            DArticulo Datos = new DArticulo();
            return Datos.Listar();
        }

        public static DataTable Buscar(string Valor)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Buscar(Valor);
        }


        public static string Insertar(string Modelo, int IdSubCategoria,int IdMarca)
        {
            DArticulo Datos = new DArticulo();

            string Existe = Datos.Existe(Modelo,IdSubCategoria,IdMarca);
            if (Existe.Equals("1"))
            {
                return "El articulo ya existe";
            }
            else
            {
                Articulo Obj = new Articulo();
                Obj.Modelo = Modelo;

                Obj.IdSubCategoria = IdSubCategoria;
                Obj.IdMarca = IdMarca;

                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int Id, int IdSubCategoria, int IdMarca, string Modelo)
        {
            DArticulo Datos = new DArticulo();
            Articulo Obj = new Articulo();

            
                string Existe = Datos.Existe(Modelo, IdSubCategoria,IdMarca);
                if (Existe.Equals("1"))
                {
                    return "El Articulo ya existe";
                }
                else
                {

                    Obj.IdArticulo = Id;
                    Obj.Modelo = Modelo;
                    Obj.IdSubCategoria = IdSubCategoria;
                    Obj.IdMarca = IdMarca;

                    return Datos.Actualizar(Obj);
                }
            

        }

        public static string Eliminar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Desactivar(Id);
        }
    }
}
