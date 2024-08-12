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
    public class NPersona
    {
        public static DataTable Listar()
        {
            DPersona Datos = new DPersona();
            return Datos.Listar();
        }

        public static DataTable Buscar(string Valor)
        {
            DPersona Datos = new DPersona();
            return Datos.Buscar(Valor);
        }

        public static string Insertar(string NombrePrimero,string NombreSegundo,string ApellidoPaterno ,string ApellidoMaterno,string Alias, string CI, string Ciudad,string Telefono)
        {
            DPersona Datos = new DPersona();

            string Existe = Datos.Existe(CI);
            if (Existe.Equals("1"))
            {
                return "La persona ya existe";
            }
            else
            {
                Persona Obj = new Persona();
                Obj.NombrePrimero = NombrePrimero;
                Obj.NombreSegundo = NombreSegundo;
                Obj.ApellidoPaterno = ApellidoPaterno;
                Obj.ApellidoMaterno = ApellidoMaterno;
                Obj.Alias = Alias;
                Obj.CI = CI;
                Obj.Telefono = Telefono;
                Obj.Ciudad = Ciudad;

                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int Id, string CIAnt, string NombrePrimero, string NombreSegundo, string ApellidoPaterno, string ApellidoMaterno, string Alias, string CI, string Ciudad, string Telefono)
        {
            DPersona Datos = new DPersona();
            Persona Obj = new Persona();

            if (CIAnt.Equals(CI))
            {
                Obj.IdPersona = Id;
                Obj.NombrePrimero = NombrePrimero;
                Obj.NombreSegundo = NombreSegundo;
                Obj.ApellidoPaterno = ApellidoPaterno;
                Obj.ApellidoMaterno = ApellidoMaterno;
                Obj.Alias = Alias;
                Obj.CI = CI;
                Obj.Telefono = Telefono;
                Obj.Ciudad = Ciudad;

                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(CI);
                if (Existe.Equals("1"))
                {
                    return "La persona ya existe";
                }
                else
                {

                    Obj.IdPersona = Id;
                    Obj.NombrePrimero = NombrePrimero;
                    Obj.NombreSegundo = NombreSegundo;
                    Obj.ApellidoPaterno = ApellidoPaterno;
                    Obj.ApellidoMaterno = ApellidoMaterno;
                    Obj.Alias = Alias;
                    Obj.CI = CI;
                    Obj.Telefono = Telefono;
                    Obj.Ciudad = Ciudad;

                    return Datos.Actualizar(Obj);
                }
            }

        }

        

        public static string Eliminar(int Id)
        {
            DPersona Datos = new DPersona();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DPersona Datos = new DPersona();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DPersona Datos= new DPersona();
            return Datos.Desactivar(Id);
        }
    }
}
