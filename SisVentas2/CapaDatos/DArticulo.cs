using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
     public class DArticulo
    {
        private int _IdArticulo;
        private string _Codigo;
        private string _Nombre;
        private string _Descripcion;
        private byte[] _imagen;
        private int _IdCategoria;
        private int _IdPresentacion;
        private string _TextoBuscar;
        
       public int IdArticulo
        {
            get{ return _IdArticulo; }
            set { _IdArticulo= value; }
        }

        public string Codigo
        {   get {return _Codigo;     }
            set{   _Codigo = value;     }
        }

        public string Nombre
        {
            get{      return _Nombre;   }
            set    {    _Nombre = value;   }
        }

        public string Descripcion
        {
            get     { return _Descripcion; }
            set  {_Descripcion = value;            }
        }

        public byte[] Imagen
        {
            get  {return _imagen;  }
            set   {  _imagen = value;}
        }

        public int IdCategoria
        {
            get   {return _IdCategoria; }
            set   { _IdCategoria = value;}
        }

        public int IdPresentacion
        {
            get{    return _IdPresentacion; }
            set  {  _IdPresentacion = value;   }
        }

        public string TextoBuscar
        {
            get { return _TextoBuscar; }
            set { _TextoBuscar = value; }
        }
        // declarando constructor
        public DArticulo()
        {

        }
        public DArticulo(int idArticulos,string codigo,string nombre,string descripcio, byte[] imagen ,int idCategoria, int idPresentacion,string textoBuscar)

        {
            this.IdArticulo = idArticulos;
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Descripcion = Descripcion;
            this.Imagen = imagen;
            this.IdCategoria = idCategoria;
            this.IdPresentacion = idPresentacion;
            this.TextoBuscar = textoBuscar;
        }

        

        public String Insertar(DArticulo Articulo)
        {
            string rpta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "PS_INSERTAR_ARTICULO";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdArticulo = new SqlParameter();
                parIdArticulo.ParameterName = "@IDARTICULO";
                parIdArticulo.SqlDbType = SqlDbType.Int;
                parIdArticulo.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(parIdArticulo);

                SqlParameter parCodigo = new SqlParameter();
                parCodigo.ParameterName = "@CODIGO";
                parCodigo.SqlDbType = SqlDbType.VarChar;
                parCodigo.Size = 50;
                parCodigo.Value = Articulo.Codigo;
                sqlcmd.Parameters.Add(parCodigo);

                SqlParameter parNombre = new SqlParameter();
                parNombre.ParameterName = "@NOMBRE";
                parNombre.SqlDbType = SqlDbType.VarChar;
                parNombre.Size = 50;
                parNombre.Value = Articulo.Nombre;
                sqlcmd.Parameters.Add(parNombre);

                SqlParameter parDescripcion = new SqlParameter();
                parDescripcion.ParameterName = "@DESCRIPCION";
                parDescripcion.SqlDbType = SqlDbType.VarChar;
                parDescripcion.Size = 250;
                parDescripcion.Value = Articulo.Descripcion;
                sqlcmd.Parameters.Add(parDescripcion);

                SqlParameter parImagen = new SqlParameter();
                parImagen.ParameterName = "@IMAGEN";
                parImagen.SqlDbType = SqlDbType.Image;
                parImagen.Value = Articulo.Imagen;
                sqlcmd.Parameters.Add(parImagen);

                SqlParameter parCategoria = new SqlParameter();
                parCategoria.ParameterName = "@IDCATEGORIA";
                parCategoria.SqlDbType = SqlDbType.Int;
                parCategoria.Value = Articulo.IdCategoria;
                sqlcmd.Parameters.Add(parCategoria);

                SqlParameter parIdPresentacion = new SqlParameter();
                parIdPresentacion.ParameterName = "@IDPRESENTACION";
                parIdPresentacion.SqlDbType = SqlDbType.Int;
                parIdPresentacion.Value = Articulo.IdPresentacion;
                sqlcmd.Parameters.Add(parIdPresentacion);



                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK":"NO SE INGRESO";
            }
            catch (Exception ex)
            {
               rpta= ex.Message;

            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open) 
                sqlcon.Close();
            }
            return rpta;
        }

        public string Editar( DArticulo Articulo)
        {
            string rpta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "PS_EDITAR_ARTICULO";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdArticulo = new SqlParameter();
                parIdArticulo.ParameterName = "@IDARTICULO";
                parIdArticulo.SqlDbType = SqlDbType.Int;
                parIdArticulo.Direction = ParameterDirection.Output;
                parIdArticulo.Value = Articulo.IdArticulo;
                sqlcmd.Parameters.Add(parIdArticulo);


                SqlParameter parCodigo = new SqlParameter();
                parCodigo.ParameterName = "@CODIGO";
                parCodigo.SqlDbType = SqlDbType.VarChar;
                parCodigo.Size = 50;
                parCodigo.Value = Articulo.Codigo;
                sqlcmd.Parameters.Add(parCodigo);

                SqlParameter parNombre = new SqlParameter();
                parNombre.ParameterName = "@NOMBRE";
                parNombre.SqlDbType = SqlDbType.VarChar;
                parNombre.Size = 50;
                parNombre.Value = Articulo.Nombre;
                sqlcmd.Parameters.Add(parNombre);

                SqlParameter parDescripcion = new SqlParameter();
                parDescripcion.ParameterName = "@DESCRIPCION";
                parDescripcion.SqlDbType = SqlDbType.VarChar;
                parDescripcion.Size = 250;
                parDescripcion.Value = Articulo.Descripcion;
                sqlcmd.Parameters.Add(parDescripcion);

                SqlParameter parImagen = new SqlParameter();
                parImagen.ParameterName = "@IMAGEN";
                parImagen.SqlDbType = SqlDbType.Image;
                parImagen.Value = Articulo.Imagen;
                sqlcmd.Parameters.Add(parImagen);

                SqlParameter parCategoria = new SqlParameter();
                parCategoria.ParameterName = "@IDCATEGORIA";
                parCategoria.SqlDbType = SqlDbType.Int;
                parCategoria.Value = Articulo.IdCategoria;
                sqlcmd.Parameters.Add(parCategoria);

                SqlParameter parIdPresentacion = new SqlParameter();
                parIdPresentacion.ParameterName = "@IDPRESENTACION";
                parIdPresentacion.SqlDbType = SqlDbType.Int;
                parIdPresentacion.Value = Articulo.IdPresentacion;
                sqlcmd.Parameters.Add(parIdPresentacion);

                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : " NO SE ALTUALIZO";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;

            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
            }
            return rpta;

        }
        
        public String Eliminar( DArticulo Articulo)
        {
            string rpta = "";
            SqlConnection sqlcon = new SqlConnection();

            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "SP_ELIMINAR_ARTICULO";
                sqlcmd.CommandType = CommandType.StoredProcedure;


                SqlParameter parIdArticulo = new SqlParameter();
                parIdArticulo.ParameterName = "@IDARTICULO";
                parIdArticulo.SqlDbType = SqlDbType.Int;
                parIdArticulo.Direction = ParameterDirection.Output;
                parIdArticulo.Value = Articulo.IdArticulo;
                sqlcmd.Parameters.Add(parIdArticulo);

                rpta = sqlcmd.ExecuteNonQuery()==1? "OK":"NO SE ELIMINO";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
             
            }
            return rpta;
        }
             
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("ARTICULO");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "PS_MOSTRAR_ARTICULO";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlDAT = new SqlDataAdapter();
                sqlDAT.Fill(DtResultado);

            }
            catch (Exception)
            {

                DtResultado=null;
            }
            return DtResultado;
        }
        public DataTable BuscarNombre(DArticulo Articulo)
        {
            DataTable DtResultado = new DataTable("ARTICULO");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "PS_BUSCAR_ARTICULO_NOMBRE";
                sqlcmd.CommandType = CommandType.StoredProcedure;


                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@TEXTOBUSCARNOMBRE";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 50;
                parTextoBuscar.Value = Articulo.IdPresentacion;
                sqlcmd.Parameters.Add(parTextoBuscar);

                SqlDataAdapter sqlDAT = new SqlDataAdapter(sqlcmd);
                sqlDAT.Fill(DtResultado);
            }

            catch (Exception )
            {
                DtResultado = null;
            }
            return DtResultado;

        }
    }
      
}
