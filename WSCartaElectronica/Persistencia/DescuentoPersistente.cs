using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSCartaElectronica.Models;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using System.Drawing;

namespace WSCartaElectronica
{
    public class DescuentoPersistente
    {
        private MySql.Data.MySqlClient.MySqlConnection conexion;



        // ------ METODOS PARA CRUD ------ \\



        public DescuentoPersistente()
        {
            string cadenaConexion = ConexionBD.cadenaConexion;

            try
            {
                conexion = new MySql.Data.MySqlClient.MySqlConnection();
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                Console.WriteLine("Error al conectarse a la base de datos " + e);
            }
        }
        public Descuento_Plato LeerDescuento(MySql.Data.MySqlClient.MySqlDataReader mySQLReader)
        {
            Descuento_Plato p = new Descuento_Plato();

            p.id_descuento = mySQLReader.GetInt32(0);
            p.nombre_descuento = mySQLReader.GetString(1);
            p.descripcion_descuento = mySQLReader.GetString(2);
            p.codigo_descuento = mySQLReader.GetString(3);
            p.precio_descuento = mySQLReader.GetInt32(4);
            p.precio_final_descuento = mySQLReader.GetDouble(5);

            p.id_plato = mySQLReader.GetInt32(6);
            p.nombre_plato = mySQLReader.GetString(7);
            p.imagen_plato = mySQLReader.GetString(8);
            p.precio_plato = mySQLReader.GetDouble(9);
            p.id_familia_plato = mySQLReader.GetInt32(10);

            return p;
        }


        



        public ArrayList ObtenerDescuentosEstablecimiento(int idioma, int establecimiento)
        {
            ArrayList arrayDescuentos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;
            
            String sqlString = "CALL Select_all_descuento_plato_establecimiento(" + idioma.ToString() + ", " + establecimiento.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {

                    Descuento_Plato p = LeerDescuento(mySQLReader);

                    arrayDescuentos.Add(p);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al hacer la consulta");

            }

            return arrayDescuentos;
        }


        public ArrayList ObtenerDescuentosUsuario(int idioma, int usuario)
        {
            ArrayList arrayDescuentos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_descuento_plato_usuario(" + idioma.ToString() + ", " + usuario.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {

                    Descuento_Plato p = LeerDescuento(mySQLReader);

                    arrayDescuentos.Add(p);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al hacer la consulta");

            }

            return arrayDescuentos;
        }



        public Int32 AñadirDescuentoUsuario(int usuario, int descuento)
        {

            Int32 operacionCompletada = -1;

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Añadir_usuario_descuento(" + usuario.ToString() + ", " + descuento.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    operacionCompletada = mySQLReader.GetInt32(0);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al hacer la consulta");
            }

            return operacionCompletada;
        }

        public void BorrarDescuentoUsuario(int usuario, int descuento)
        {

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "Delete from usuario_descuento where id_usuario = " + usuario + " and id_descuento = " + descuento + ";";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                Console.WriteLine("Error al hacer la consulta");
            }

        }




    }
}