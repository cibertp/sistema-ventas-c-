using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
   public  class Dcategoria
    {
        private int _IdCategoria;
        private String _Nombre;
        private String _Descripcion;
        private string _TextoBuscar;
        // REFACTORIZANDO Y ENCAPSULANOD CAMPO
        // y declarando get y set
        public int IdCategoria
        {
            get { return _IdCategoria; }
            set { _IdCategoria = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public string TextoBuscar
        {
            get { return _TextoBuscar; }
            set { _TextoBuscar = value; }
        }

        public Dcategoria()
        {
            // este es elconstrucror
        }
        public Dcategoria(int idCategoria, String nombre, string descripcion, String textoBuscar)
        {
            this.IdCategoria = idCategoria;
            this.Nombre = nombre;
            this._Descripcion = descripcion;
            this._TextoBuscar = textoBuscar;
        }
        // metodo para insert eliminar
        //con DCategoria esta instancia para recibir a manera de objeto
        public string Insertar(Dcategoria Categoria)
        {
            string rpta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                sqlcon.Open();
                // estableciendo conexion
                SqlCommand Sqlcmd = new SqlCommand();
                Sqlcmd.Connection = sqlcon;
                Sqlcmd.CommandText = "SP_INSERTAR_CATEGORIA";
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@IDCATEGORIA";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Direction = ParameterDirection.Output;
                Sqlcmd.Parameters.Add(parIdCategoria);

                SqlParameter parNombre = new SqlParameter();
                parNombre.ParameterName = "@NOMBRE";
                parNombre.SqlDbType = SqlDbType.VarChar;
                parNombre.Size = 50;
                parNombre.Value = Categoria.Nombre;
                Sqlcmd.Parameters.Add(parNombre);

                SqlParameter parDecripcion = new SqlParameter();
                parDecripcion.ParameterName = "@DESCRIPCION";
                parDecripcion.SqlDbType = SqlDbType.VarChar;
                parDecripcion.Size = 250;
                parDecripcion.Value = Categoria.Descripcion;
                Sqlcmd.Parameters.Add(parDecripcion);

                // EJECUTANDO  COMANDOS
                rpta = Sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingreso el registro";
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
        public string Editar(Dcategoria Categoria)
        {
            string rpta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "SP_EDITAR_CATEGORIA";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@IDCATEGORIA";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = Categoria.IdCategoria;
                sqlcmd.Parameters.Add(parIdCategoria);

                SqlParameter parNombre = new SqlParameter();
                parNombre.ParameterName = "@NOMBRE";
                parNombre.SqlDbType = SqlDbType.VarChar;
                parNombre.Size = 50;
                parNombre.Value = Categoria.Nombre;
                sqlcmd.Parameters.Add(parNombre);

                SqlParameter parDescripcion = new SqlParameter();
                parDescripcion.ParameterName = "@DESCRIPCION";
                parDescripcion.SqlDbType = SqlDbType.VarChar;
                parDescripcion.Size = 250;
                parDescripcion.Value = Categoria.Descripcion;
                sqlcmd.Parameters.Add(parDescripcion);

                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "no se actualizo";

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

        public string Eliminar(Dcategoria Categoria)
        {
          string  rpta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "SP_ELIMINAR_CATEGORIA";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdcategoria = new SqlParameter();
                parIdcategoria.ParameterName = "@IDCATEGORIA";
                parIdcategoria.SqlDbType = SqlDbType.Int;
                parIdcategoria.Value = Categoria.IdCategoria;

                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "ok" : "no se elimino";


            }catch(Exception ex)
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
        //para mostrarme toda las filas de mi tabla 
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("CATEGORIA");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "SP_MOSTRAR_CATEGORIA";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //para ejecutar el comando u llenar el fomulario
                SqlDataAdapter sqldat = new SqlDataAdapter(sqlcmd);
                sqldat.Fill(DtResultado);
            }
            catch(Exception )
            {
                DtResultado = null;
            }
            return DtResultado;

        }
        public DataTable BuscarNombre(Dcategoria Categoria)
        {

            DataTable DtResultado = new DataTable("CATEGORIA");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "SP_BUSCAR_CATEGORIA";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextBuscar = new SqlParameter();
                parTextBuscar.ParameterName = "@TEXTOBUSCAR";
                parTextBuscar.SqlDbType = SqlDbType.VarChar;
                parTextBuscar.Size = 50;
                parTextBuscar.Value = Categoria.TextoBuscar;
                sqlcmd.Parameters.Add(parTextBuscar);

                SqlDataAdapter sqlDat = new SqlDataAdapter();
                sqlDat.Fill(DtResultado);

            }catch(Exception )
            {
                DtResultado = null;
            }
            return DtResultado;

        }



    }
}
