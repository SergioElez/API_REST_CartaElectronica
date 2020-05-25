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
    public class PlatoPersistente
    {
        private MySql.Data.MySqlClient.MySqlConnection conexion;



        // ------ METODOS PARA CRUD ------ \\



        public PlatoPersistente()
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
        public Plato LeerPlato(MySql.Data.MySqlClient.MySqlDataReader mySQLReader)
        {
            Plato p = new Plato();

            p.id = mySQLReader.GetInt32(0);
            p.nombre = mySQLReader.GetString(1);
            p.descripcion = mySQLReader.GetString(2);
            p.imagen = mySQLReader.GetString(3);
            p.precio = mySQLReader.GetDouble(4);
            p.id_familia = mySQLReader.GetInt32(5);

            return p;
        }


        public Plato ObtenerPlato(int idioma, int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_plato(" + idioma.ToString() + ", " + id.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            //Si hay un error al hacer la consulta que devuelva null
            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    return LeerPlato(mySQLReader);

                }
                else
                {
                    conexion.Close();
                    return null;
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


        }
        
        public ArrayList ObtenerPlatos(int idioma)
        {
            ArrayList arrayPlatos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_plato(" + idioma.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    Plato p = new Plato();

                    p.id = mySQLReader.GetInt32(0);
                    p.nombre = mySQLReader.GetString(1);
                    p.descripcion = "";
                    p.imagen = mySQLReader.GetString(2);
                    p.precio = mySQLReader.GetDouble(3);
                    p.id_familia = mySQLReader.GetInt32(4);

                    arrayPlatos.Add(p);
                }
            }
            catch (Exception)
            {
                conexion.Close();
                Console.WriteLine("Error al hacer la consulta");
            }
            finally
            {
                conexion.Close();
            }

            return arrayPlatos;
        }



        public long GuardarPlato(PlatoTraducido plato)
        {

            String sqlString = "CALL Crear_plato('" +
                plato.nombre_ES + "', '" +
                plato.descripcion_ES + "', '" +
                plato.nombre_EN + "', '" +
                plato.descripcion_EN + "', '" +
                plato.imagen + "', " +
                plato.precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", "+
                plato.id_familia.ToString() + ");";

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);
            Console.WriteLine(sqlString);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conexion.Close();
                Console.WriteLine("Error al insertar Plato: " + cmd.LastInsertedId);
            }
            finally
            {
                conexion.Close();
            }


            long id = cmd.LastInsertedId;
            return id;
        }

        public bool ActualizarPlato(int idioma, PlatoTraducido plato)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;


            String sqlString = "CALL Select_all_plato(" + idioma + ");";


            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mySQLReader.Close();

                    sqlString = "CALL Actualizar_plato(" +
                       plato.id + ", '" +
                       plato.nombre_ES + "', '" +
                       plato.descripcion_ES + "', '" +
                       plato.nombre_EN + "', '" +
                       plato.descripcion_EN + "', '" +
                       plato.imagen + "', " +
                       plato.precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", " +
                       plato.id_familia.ToString() + ");";



                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

                    cmd.ExecuteNonQuery();

                    return true;
                }
                else
                {
                    conexion.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al hacer la consulta");
                conexion.Close();
                return false;
            }
            finally
            {
                conexion.Close();
            }

        }

        // DE MOMENTO NO VAMOS A BORRAR NADA EN LA BD, LO DE ABAJO NO FUNCIONA
        public bool BorrarPlato(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "SELECT * FROM Plato WHERE codigo = " + id.ToString();

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mySQLReader.Close();

                    sqlString = "DELETE FROM Plato WHERE codigo = " + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

                    cmd.ExecuteNonQuery();

                    return true;
                }
                else
                {
                    conexion.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al hacer la consulta");
                conexion.Close();
                return false;
            }
            finally
            {
                conexion.Close();
            }

        }


        // ------ METODOS EXTRA ----- \\

        public ArrayList BuscarPlatosPorFamilia(int idioma, int familia)
        {
            ArrayList arrayPlatos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_plato_familia(" + idioma + ", " + familia + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    Plato p = new Plato();

                    p.id = mySQLReader.GetInt32(0);
                    p.nombre = mySQLReader.GetString(1);
                    p.descripcion = "";
                    p.imagen = mySQLReader.GetString(2);
                    p.precio = mySQLReader.GetDouble(3);
                    p.id_familia = familia;

                    arrayPlatos.Add(p);
                }

            }
            catch (Exception)
            {
                conexion.Close();
                Console.WriteLine("Error al hacer la consulta");
            }
            finally
            {
                conexion.Close();
            }
            return arrayPlatos;

        }


        public ArrayList BuscarPlatosPorNombre(int idioma, int familia, string busqueda)
        {
            ArrayList arrayPlatos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_plato_familia_nombre(" + idioma + ", " + familia + ", '%" + busqueda + "%');";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    Plato p = new Plato();

                    p.id = mySQLReader.GetInt32(0);
                    p.nombre = mySQLReader.GetString(1);
                    p.descripcion = "";
                    p.imagen = mySQLReader.GetString(2);
                    p.precio = mySQLReader.GetDouble(3);
                    p.id_familia = familia;

                    arrayPlatos.Add(p);
                }
            }
            catch (Exception)
            {
                conexion.Close();
                Console.WriteLine("Error al hacer la consulta");
            }
            finally
            {
                conexion.Close();
            }


            return arrayPlatos;
        }

        public ArrayList BuscarPlatosPorTagYFamilia(int idioma, string tag, int familia)
        {
            ArrayList arrayPlatos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_plato_tag_familia(" + idioma + ", '" + tag + "', " + familia + ");";


            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {

                    Plato p = new Plato();

                    p.id = mySQLReader.GetInt32(0);
                    p.nombre = mySQLReader.GetString(1);
                    p.descripcion = "";
                    p.imagen = mySQLReader.GetString(2);
                    p.precio = mySQLReader.GetDouble(3);
                    p.id_familia = familia;

                    arrayPlatos.Add(p);
                }
            }
            catch (Exception)
            {
                conexion.Close();
                Console.WriteLine("Error al hacer la consulta");
            }
            finally
            {
                conexion.Close();
            }


            return arrayPlatos;
        }

        public ArrayList BuscarPlatosPorTagFamiliaYNombre(int idioma, string tag, int familia, string busqueda)
        {
            ArrayList arrayPlatos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_plato_tag_familia_nombre(" + idioma + ", '" + tag + "', " + familia + ", '%" + busqueda+ "%');";


            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {

                    Plato p = new Plato();

                    p.id = mySQLReader.GetInt32(0);
                    p.nombre = mySQLReader.GetString(1);
                    p.descripcion = "";
                    p.imagen = mySQLReader.GetString(2);
                    p.precio = mySQLReader.GetDouble(3);
                    p.id_familia = familia;

                    arrayPlatos.Add(p);
                }
            }
            catch (Exception)
            {
                conexion.Close();
                Console.WriteLine("Error al hacer la consulta");
            }
            finally
            {
                conexion.Close();
            }


            return arrayPlatos;
        }


        public ArrayList ObtenerAlergenosPorPlato(int idioma, int plato)
        {
            ArrayList arrayAlergenos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_alergeno_ingrediente_plato(" + idioma + ", " + plato + ");";


            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {

                    Alergeno p = new Alergeno();

                    p.id = mySQLReader.GetInt32(0);
                    p.id_ingrediente = mySQLReader.GetInt32(1);
                    p.nombre = mySQLReader.GetString(2);

                    arrayAlergenos.Add(p);
                }

            }
            catch (Exception)
            {
                conexion.Close();
                Console.WriteLine("Error al hacer la consulta");
            }
            finally
            {
                conexion.Close();
            }
            return arrayAlergenos;
        }

        public Maridaje ObtenerPlatoMaridaje(int idioma, int plato)
        {
            Maridaje maridaje = new Maridaje();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_maridaje_plato(" + idioma + ", " + plato + ");";


            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);


            try
            {
                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {

                    maridaje.id = -1;
                    maridaje.id_plato = plato;
                    maridaje.id_plato_recomendado = mySQLReader.GetInt32(0);
                    maridaje.descripcion = mySQLReader.GetString(1);

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al hacer la consulta");
                conexion.Close();
            }
            finally
            {
                conexion.Close();
            }

            
            return maridaje;
        }




    }
}