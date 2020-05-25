using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSCartaElectronica.Models;
using System.Collections;



namespace WSCartaElectronica
{
    public class FamiliaPersistente
    {
        private MySql.Data.MySqlClient.MySqlConnection conexion;



        // ------ METODOS PARA CRUD ------ \\


        public FamiliaPersistente()
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
                conexion.Close();
                Console.WriteLine("Error al conectarse a la base de datos " + e);
            }
        }
        public Familia LeerFamilia(MySql.Data.MySqlClient.MySqlDataReader mySQLReader)
        {
            Familia p = new Familia();

            p.id = mySQLReader.GetInt32(0);
            p.nombre = mySQLReader.GetString(1);
            p.imagen = mySQLReader.GetString(2);

            return p;
        }


        public Familia ObtenerFamilia(int idioma, int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_Familia(" + idioma.ToString() + ", " + id.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            //Si hay un error al hacer la consulta que devuelva null
            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    return LeerFamilia(mySQLReader);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                conexion.Close();
                return null;
            }
            finally
            {
                conexion.Close();
            }
        }
        
        public ArrayList ObtenerFamilias(int idioma)
        {
            ArrayList arrayFamilias = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_Familia(" + idioma.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    Familia p = new Familia();

                    p.id = mySQLReader.GetInt32(0);
                    p.nombre = mySQLReader.GetString(1);
                    p.imagen = mySQLReader.GetString(2);

                    arrayFamilias.Add(p);
                }
            }
            catch (Exception e)
            {
                conexion.Close();
                return null;
            }
            finally
            {
                conexion.Close();
            }


            return arrayFamilias;
        }


        //// ------ METODOS EXTRA ----- \\

        public ArrayList BuscarFamiliaPorEstablecimiento(int idioma, int establecimiento)
        {
            ArrayList arrayFamilias = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_familia_establecimiento(" + idioma + ", " + establecimiento + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);



            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    Familia p = new Familia();

                    p.id = mySQLReader.GetInt32(0);
                    p.nombre = mySQLReader.GetString(1);
                    p.imagen = mySQLReader.GetString(2);

                    arrayFamilias.Add(p);
                }
            }
            catch (Exception e)
            {
                conexion.Close();
                return null;
            }
            finally
            {
                conexion.Close();
            }


            return arrayFamilias;

        }

        public ArrayList BuscarFamiliasPorNombre(int idioma, int establecimiento, string busqueda)
        {
            ArrayList arrayFamilias = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_familia_establecimiento_nombre(" + idioma + ", " + establecimiento + ", '%" + busqueda + "%');";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    Familia p = new Familia();

                    p.id = mySQLReader.GetInt32(0);
                    p.nombre = mySQLReader.GetString(1);
                    p.imagen = mySQLReader.GetString(2);

                    arrayFamilias.Add(p);
                }
            }
            catch (Exception e)
            {
                conexion.Close();
                return null;
            }
            finally
            {
                conexion.Close();
            }


            return arrayFamilias;
        }

    }
}