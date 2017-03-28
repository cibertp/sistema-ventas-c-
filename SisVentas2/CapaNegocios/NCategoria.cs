using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;

namespace CapaNegocios
{
   public class NCategoria
    {
        
        // metodo insert que esta llamando el metodo de clasel Dcategoria de dla capa datos
        public static string Insertar(string nombre,string descripcion)
        {
           Dcategoria obj = new Dcategoria();
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;
            return obj.Insertar(obj);
        }

        public static string Editar(int idCategoria,String nombre,string descripcion )
        {
            Dcategoria obj = new Dcategoria();
            obj.IdCategoria = idCategoria;
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;
            return obj.Editar(obj);

        }

        public static string Eliminar(int idCategoria)
        {
            Dcategoria obj = new Dcategoria();
            obj.IdCategoria = idCategoria;
            return obj.Eliminar(obj);
        }

        public static DataTable Mostrar()
        {
            //Dcategoria obj = new Dcategoria();
            //obj.Mostrar();
            return new Dcategoria().Mostrar();
        }
        public static DataTable BuscarNombre(string textBuscar)
        {
            Dcategoria obj = new Dcategoria();
            obj.TextoBuscar = textBuscar;

            return obj.BuscarNombre(obj);

        }

    }
}
