using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Servicios_Web_Calidad
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string crearUsuarios(string us_nom, string us_contra, string us_correo)
        {
            Oracle_conection conn = new Oracle_conection();
            conn.EstablecerConnection();

            Procedimientos pc = new Procedimientos();


            return pc.crearUsuarios(conn.GetConexion(), us_nom, us_contra, us_contra);

        }
        [WebMethod]
        public string listarUsuarios()
        {
            Oracle_conection conn = new Oracle_conection();
            conn.EstablecerConnection();

            Procedimientos pc = new Procedimientos();

            return pc.listarUsuarios(conn.GetConexion());

        }
        [WebMethod]
        public string actualizarUsuario(string us_correo, string us_nom, string us_contrasena)
        {
            Oracle_conection conn = new Oracle_conection();
            conn.EstablecerConnection();

            Procedimientos pc = new Procedimientos();


            return pc.actualizarUsuario(conn.GetConexion(), us_correo, us_nom, us_contrasena);


        }
        [WebMethod]
        public string eliminarUsuario(string us_correo)
        {
            Oracle_conection conn = new Oracle_conection();
            conn.EstablecerConnection();

            Procedimientos pc = new Procedimientos();


            return pc.eliminarUsuario(conn.GetConexion(), us_correo);

        }
        [WebMethod]
        public string validarUsuario(string us_correo , string us_contraseña)
        {
            Oracle_conection conn = new Oracle_conection();
            conn.EstablecerConnection();

            Procedimientos pc = new Procedimientos();


            return pc.validarUsuario(conn.GetConexion(), us_correo, us_contraseña);

        }
    }
}
