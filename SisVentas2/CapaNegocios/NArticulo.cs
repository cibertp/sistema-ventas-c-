using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class NArticulo
    {

        public static string Insertar(string codigo, string nombre, string descripcion, byte[] imagen, int idCategoria, int idPresentacion)
        {
            DArticulo OBJ = new DArticulo();
            OBJ.Codigo = codigo;
            OBJ.Nombre = nombre;
            OBJ.Descripcion = descripcion;
            OBJ.Imagen = imagen;
            OBJ.IdCategoria = idCategoria;
            OBJ.IdPresentacion = idPresentacion;
          

            return OBJ.Insertar(OBJ);

        }
        public static String Editar(int idArticulo, string codigo, string nombre, string descripcion, byte[] imagen, int idCategoria, int idPresentacion)
        {
            DArticulo OBJ = new DArticulo();
            OBJ.IdArticulo = idArticulo;
            OBJ.Codigo = codigo;
            OBJ.Nombre = nombre;
            OBJ.Descripcion = descripcion;
            OBJ.Imagen = imagen;
            OBJ.IdCategoria = idCategoria;
            OBJ.IdPresentacion = idPresentacion;

            return OBJ.Editar(OBJ);

        }
        public static string Eliminar(int idArticulo)
        {
            DArticulo OBJ = new DArticulo();
            OBJ.IdArticulo = idArticulo;

            return OBJ.Eliminar(OBJ);
        }
        public static DataTable Mostrar()
        {
            return new DArticulo().Mostrar();
        }
        
        public static DataTable BuscarNombre(String textoBuscar)
        {
            DArticulo OBJ = new DArticulo();
            OBJ.TextoBuscar = textoBuscar;
            return OBJ.BuscarNombre(OBJ);
            
        }     

    }
}
