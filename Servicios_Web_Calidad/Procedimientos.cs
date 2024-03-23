using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Servicios_Web_Calidad
{
    public class Procedimientos
    {
        public string crearUsuarios(OracleConnection conn, string us_nom, string us_correo, string us_contraseña)
        {

            OracleParameter user_name = new OracleParameter();
            user_name.OracleDbType = OracleDbType.Varchar2;
            user_name.Value = us_nom;

            OracleParameter user_cor = new OracleParameter();
            user_cor.OracleDbType = OracleDbType.Varchar2;
            user_cor.Value = us_correo;


            OracleParameter user_contra = new OracleParameter();
            user_contra.OracleDbType = OracleDbType.Varchar2;
            user_contra.Value = us_contraseña;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "crearUsuarios";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("Nombre_Completo:", OracleDbType.Varchar2).Value = user_name.Value;
            cmd.Parameters.Add("Correo:", OracleDbType.Varchar2).Value = user_cor.Value;
            cmd.Parameters.Add("Contraseña:", OracleDbType.Varchar2).Value = user_contra.Value;
            cmd.ExecuteNonQuery();

            string respuesta = "Uusuario creado";
            conn.Dispose();

            return respuesta;
        }
        public string listarUsuarios(OracleConnection conn)
        {

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;


            OracleParameter param_cursor = new OracleParameter();
            param_cursor.OracleDbType = OracleDbType.RefCursor;

            cmd.CommandText = "listarUsuarios";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("usr_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;



            DataSet ds = new DataSet();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);

            return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
        }


        public string actualizarUsuario(OracleConnection conn, string us_correo, string us_nom, string us_contrasena)
        {

            OracleParameter user_nom = new OracleParameter();
            user_nom.OracleDbType = OracleDbType.Varchar2;
            user_nom.Value = us_nom;

            OracleParameter user_correo = new OracleParameter();
            user_correo.OracleDbType = OracleDbType.Varchar2;
            user_correo.Value = us_correo;

            OracleParameter user_contra = new OracleParameter();
            user_contra.OracleDbType = OracleDbType.Varchar2;
            user_contra.Value = us_contrasena;


            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "actualizarUsuario";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("Correo:", OracleDbType.Varchar2).Value = user_correo.Value;
            cmd.Parameters.Add("Nombre_Completo(Nuevo):", OracleDbType.Varchar2).Value = user_nom.Value;
            cmd.Parameters.Add("Contraseña(Nuevo):", OracleDbType.Varchar2).Value = user_contra.Value;
            cmd.ExecuteNonQuery();

            string respuesta = "Usuario Actualizado";
            conn.Dispose();

            return respuesta;
        }
       
        public string eliminarUsuario(OracleConnection conn, string us_correo)
        {

            OracleParameter user_id = new OracleParameter();
            user_id.OracleDbType = OracleDbType.Varchar2;
            user_id.Value = us_correo;


            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "eliminarUsuario";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("Correo:", OracleDbType.Varchar2).Value = user_id.Value;

            cmd.ExecuteNonQuery();

            string respuesta = "Usuario Eliminado";
            conn.Dispose();

            return respuesta;
        }
        public string validarUsuario(OracleConnection conn, string us_correo, string us_contraseña)
        {
            string respuesta = "";

            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "validarUsuario";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Parámetros de entrada
                cmd.Parameters.Add("us_correo", OracleDbType.Varchar2).Value = us_correo;
                cmd.Parameters.Add("us_contraseña", OracleDbType.Varchar2).Value = us_contraseña;

                // Parámetro de salida
                OracleParameter outputParam = new OracleParameter();
                outputParam.ParameterName = "v_respuesta";
                outputParam.OracleDbType = OracleDbType.Varchar2;
                outputParam.Size = 100; // Tamaño adecuado para el mensaje de respuesta
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                cmd.ExecuteNonQuery();

                // Obtener el valor del parámetro de salida
                respuesta = outputParam.Value.ToString();
            }
            catch (Exception ex)
            {
                respuesta = "Error al validar el usuario: " + ex.Message;
            }
            finally
            {
                conn.Dispose();
            }

            return respuesta;
        }
    }
}