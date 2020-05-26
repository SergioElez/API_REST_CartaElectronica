using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSCartaElectronica.Models;
using System.Collections;



namespace WSCartaElectronica
{
    public class TagPersistente
    {
        private MySql.Data.MySqlClient.MySqlConnection conexion;



        // ------ METODOS PARA CRUD ------ \\



        public TagPersistente()
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
        public Tag LeerTag(MySql.Data.MySqlClient.MySqlDataReader mySQLReader)
        {
            Tag p = new Tag();

            p.id = mySQLReader.GetInt32(0);
            p.nombre= mySQLReader.GetString(1);
            p.color = mySQLReader.GetString(2);

            return p;
        }


        public Tag ObtenerTag(int idioma, int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_Tag(" + idioma.ToString() + ", " + id.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            //Si hay un error al hacer la consulta que devuelva null
            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    return LeerTag(mySQLReader);
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
        
        public ArrayList ObtenerTagsDeUnPlato(int idioma, int plato)
        {
            ArrayList arrayTags = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_tag_plato(" + idioma.ToString() + ", " + plato + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            
            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    Tag p = LeerTag(mySQLReader);

                    arrayTags.Add(p);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                Console.WriteLine("Error al conectarse a la base de datos " + e);
            }
            finally
            {
                conexion.Close();
            }


            return arrayTags;
        }

        public ArrayList ObtenerTags(int idioma)
        {
            ArrayList arrayTags = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_tag(" + idioma + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    Tag p = LeerTag(mySQLReader);

                    arrayTags.Add(p);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                Console.WriteLine("Error al conectarse a la base de datos " + e);
            }
            finally
            {
                conexion.Close();
            }


            return arrayTags;
        }


    }
}