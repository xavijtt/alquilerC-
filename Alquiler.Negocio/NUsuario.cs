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
    public class NUsuario
    {
        public static DataTable Listar()
        {
            DUsuario Datos = new DUsuario();
            return Datos.Listar();
        }

        public static DataTable Buscar(string Valor)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Buscar(Valor);
        }

        public static DataTable Login(string User,string Clave)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Login(User,Clave);
        }


        public static string Insertar(string User, string Clave, int IdRol)
        {
            DUsuario Datos = new DUsuario();

            string Existe = Datos.Existe(User);
            if (Existe.Equals("1"))
            {
                return "El usuario ya existe";
            }
            else
            {
                Usuario Obj = new Usuario();
                Obj.User = User;
                Obj.Clave= Clave;
                Obj.IdRol = IdRol;

                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int Id, int IdRol, string NombreAnt, string User, string Clave)
        {
            DUsuario Datos = new DUsuario();
            Usuario Obj = new Usuario();

            if (NombreAnt.Equals(User))
            {
                Obj.IdUsuario = Id;
                Obj.User = User;
                Obj.Clave = Clave;
                Obj.IdRol = IdRol;

                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(User);
                if (Existe.Equals("1"))
                {
                    return "El usuario ya existe";
                }
                else
                {

                    Obj.IdUsuario = Id;
                    Obj.User = User;
                    Obj.Clave = Clave;
                    Obj.IdRol = IdRol;

                    return Datos.Actualizar(Obj);
                }
            }

        }

        public static string Eliminar(int Id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Desactivar(Id);
        }
    }
}
