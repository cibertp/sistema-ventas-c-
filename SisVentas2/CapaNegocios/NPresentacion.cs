using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data.SqlClient;
using System.Data;

namespace CapaNegocios

{
    public class NPresentacion
    {
        public static string Insertar(string nombre, string Descripcion)
        {
            DPresentacion obj = new DPresentacion();
            obj.Nombre = nombre;
            obj.Descripcion = Descripcion;
            return obj.Insertar(obj);
        }

        public static string Editar(int idPresentacion, string nombre, string descripcion)
        {
            DPresentacion obj = new DPresentacion();
            obj.IdPresentacion = idPresentacion;
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;
            return obj.Editar(obj);
        }
        public static string Eliminar(int idPresentacion)
        {
            DPresentacion obj = new DPresentacion();
            obj.IdPresentacion = idPresentacion;

            return obj.Eliminar(obj);
        }
        public static DataTable Mostrar()
        {
            return new DPresentacion().Mostrar();

        }
        public static DataTable BuscarNombre(String textoBuscar)
        {
            DPresentacion obj = new DPresentacion();
            obj.TextoBuscar = textoBuscar;
            return obj.BuscarNombre(obj);
        }

    }
}
   
