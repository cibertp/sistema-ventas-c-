using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DPresentacion
    {
        private int _IdPresentacion ;
        private string _Nombre;
        private string _Descripcion;
        private string _TextoBuscar;

        public int IdPresentacion
        {
            get{return _IdPresentacion; }
             set{_IdPresentacion = value; }
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

        public DPresentacion()
        {

        }
        public DPresentacion(int idPresentacion,string nombre,string descripcion,String textoBuscar)
        {
            this.IdPresentacion = idPresentacion;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.TextoBuscar = textoBuscar;
        }
        public String Insertar (DPresentacion Presentacion)
        {
                string rpta="";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "SP_INSERTAR_PRESENTACION";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdPresentacion = new SqlParameter();
                parIdPresentacion.ParameterName = "@IDPRESENTACION";
                parIdPresentacion.SqlDbType = SqlDbType.Int;
                parIdPresentacion.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(parIdPresentacion);

                SqlParameter parNombre = new SqlParameter();
                parNombre.ParameterName = "@NOMBRE";
                parNombre.SqlDbType = SqlDbType.VarChar;
                parNombre.Size = 50;
                parNombre.Value = Presentacion.Nombre;
                sqlcmd.Parameters.Add(parNombre);

                SqlParameter parDescripcion = new SqlParameter();
                parDescripcion.ParameterName = "@DESCRIPCION";
                parDescripcion.SqlDbType = SqlDbType.VarChar;
                parDescripcion.Size = 250;
                parDescripcion.Value = Presentacion.Descripcion;
                sqlcmd.Parameters.Add(parDescripcion);

                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK":"No se registro";

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
        public string Editar( DPresentacion Presentacion)
        {
            String rpta = "";
            SqlConnection sqlcon = new SqlConnection();           
            try               
            {
                sqlcon.ConnectionString = Conexion.Cn;
                sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "SP_EDITAR_PRESENTACION";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIDpresentacion = new SqlParameter();
                parIDpresentacion.ParameterName = "@IDPRESENTACION";
                parIDpresentacion.SqlDbType = SqlDbType.Int;
                parIDpresentacion.Value = Presentacion.IdPresentacion;
                sqlcmd.Parameters.Add(parIDpresentacion);

                SqlParameter parNombre = new SqlParameter();
                parNombre.ParameterName = "@NOMBRE";
                parNombre.SqlDbType = SqlDbType.VarChar;
                parNombre.Size = 50;
                parNombre.Value = Presentacion.Nombre;
                sqlcmd.Parameters.Add(parNombre);

                SqlParameter parDescripcion = new SqlParameter();
                parDescripcion.ParameterName = "@DESCRIPCION";
                parDescripcion.SqlDbType = SqlDbType.VarChar;
                parDescripcion.Size = 250;
                parDescripcion.Value = Presentacion.Descripcion;
                sqlcmd.Parameters.Add(parDescripcion);

                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE EDITO";
                
            }catch(Exception EX)
            {
                rpta = EX.Message;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
            }

            return rpta;

        }

            
        public string Eliminar(DPresentacion Presentacion)
        {
            String rpta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;
                sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "SP_ELIMINAR_PRESENTACION";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdPresentacion = new SqlParameter();
                parIdPresentacion.ParameterName = "@IDPRESENTACION";
                parIdPresentacion.SqlDbType = SqlDbType.Int;
                parIdPresentacion.Value = Presentacion.IdPresentacion;
                sqlcmd.Parameters.Add(parIdPresentacion);


                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : " NO SE ELIMINO";


            }
            catch (Exception EX)
            {
                rpta = EX.Message;
                                
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open) 
                sqlcon.Close();
            }
            return rpta;
        }
       
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("PRESENTACION");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandText = "PS_MOSTRAR_PRESENTACION";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlcmd);
                sqlDat.Fill(DtResultado);
                
            }
            catch (Exception)
            {
                DtResultado = null;
            }
            return DtResultado;
        }


        public DataTable BuscarNombre(DPresentacion Presentacion)
        {
            DataTable DtResultado = new DataTable("PRESENTACION");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Cn;

                SqlCommand sqlcmd = new SqlCommand();

                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = "SP_BUSCAR_PRESENTACION";
                sqlcmd.CommandType = CommandType.StoredProcedure;


                SqlParameter parTextBuscar = new SqlParameter();
                parTextBuscar.ParameterName = "@TEXTOBUSCAR";
                parTextBuscar.SqlDbType = SqlDbType.VarChar;
                parTextBuscar.Size = 50;
                parTextBuscar.Value = Presentacion.TextoBuscar;
                sqlcmd.Parameters.Add(parTextBuscar);

                SqlDataAdapter sqlDAT = new SqlDataAdapter(sqlcmd);
                sqlDAT.Fill(DtResultado);
            }
            catch (Exception)
            {
                DtResultado = null;                        
            }
            return DtResultado;
        }
        
    }
}
