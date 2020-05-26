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
    public class EstablecimientoPersistente
    {
        private MySql.Data.MySqlClient.MySqlConnection conexion;



        // ------ METODOS PARA CRUD ------ \\



        public EstablecimientoPersistente()
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
        public Establecimiento LeerEstablecimiento(MySql.Data.MySqlClient.MySqlDataReader mySQLReader)
        {
            Establecimiento p = new Establecimiento();

            p.id = mySQLReader.GetInt32(0);
            p.nombre = mySQLReader.GetString(1);
            p.descripcion = mySQLReader.GetString(2);
            p.tipo = mySQLReader.GetString(3);
            p.mapa = mySQLReader.GetString(4);
            p.imagen = mySQLReader.GetString(5);
            p.id_empresa = mySQLReader.GetInt32(6);
            return p;
        }


        public Establecimiento ObtenerEstablecimiento(int idioma, int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_Establecimiento(" + idioma.ToString() + ", " + id.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            //Si hay un error al hacer la consulta que devuelva null
            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    return LeerEstablecimiento(mySQLReader);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }
        
        public ArrayList ObtenerEstablecimientos(int idioma)
        {
            ArrayList arrayEstablecimientos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_Establecimiento(" + idioma.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                Establecimiento p = LeerEstablecimiento(mySQLReader);

                arrayEstablecimientos.Add(p);
            }

            return arrayEstablecimientos;
        }

        public string ObtenerMapaEstablecimiento(int idioma, int id)
        {
            string mapa;

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_Establecimiento(" + idioma.ToString() + ", " + id.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            //Si hay un error al hacer la consulta que devuelva null
            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mapa = mySQLReader.GetString(4);
                    return mapa;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }



    }
}