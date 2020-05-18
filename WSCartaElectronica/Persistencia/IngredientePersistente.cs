using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSCartaElectronica.Models;
using System.Collections;



namespace WSCartaElectronica
{
    public class IngredientePersistente
    {
        private MySql.Data.MySqlClient.MySqlConnection conexion;



        // ------ METODOS PARA CRUD ------ \\



        public IngredientePersistente()
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
        public Ingrediente LeerIngrediente(MySql.Data.MySqlClient.MySqlDataReader mySQLReader)
        {
            Ingrediente p = new Ingrediente();

            p.id = mySQLReader.GetInt32(0);
            p.nombre = mySQLReader.GetString(1);

            return p;
        }


        public Ingrediente ObtenerIngrediente(int idioma, int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_Ingrediente(" + idioma.ToString() + ", " + id.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            //Si hay un error al hacer la consulta que devuelva null
            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    return LeerIngrediente(mySQLReader);
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
        
        public ArrayList ObtenerIngredientesDeUnPlato(int idioma, int plato)
        {
            ArrayList arrayIngredientes = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_Ingrediente_plato(" + idioma.ToString() + ", " + plato + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                //EDITADO
                Ingrediente p = LeerIngrediente(mySQLReader);

                arrayIngredientes.Add(p);
            }

            return arrayIngredientes;
        }


        //public long GuardarIngrediente(IngredienteTraducido Ingrediente)
        //{

        //    String sqlString = "CALL Crear_Ingrediente('" +
        //        Ingrediente.nombre_ES + "', '" +
        //        Ingrediente.descripcion_ES + "', '" +
        //        Ingrediente.nombre_EN + "', '" +
        //        Ingrediente.descripcion_EN + "', '" +
        //        Ingrediente.imagen + "', " +
        //        Ingrediente.precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", "+
        //        Ingrediente.id_Ingrediente.ToString() + ");";

        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);
        //    Console.WriteLine(sqlString);
        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Error al insertar Ingrediente: " + cmd.LastInsertedId);
        //    }


        //    long id = cmd.LastInsertedId;
        //    return id;
        //}

        //public bool ActualizarIngrediente(int idioma, IngredienteTraducido Ingrediente)
        //{
        //    MySql.Data.MySqlClient.MySqlDataReader mySQLReader;


        //    String sqlString = "CALL Select_all_Ingrediente(" + idioma + ");";


        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //    mySQLReader = cmd.ExecuteReader();
        //    if (mySQLReader.Read())
        //    {
        //        mySQLReader.Close();

        //         sqlString = "CALL Actualizar_Ingrediente(" +
        //            Ingrediente.id+ ", '" +
        //            Ingrediente.nombre_ES + "', '" +
        //            Ingrediente.descripcion_ES + "', '" +
        //            Ingrediente.nombre_EN + "', '" +
        //            Ingrediente.descripcion_EN + "', '" +
        //            Ingrediente.imagen + "', " +
        //            Ingrediente.precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", " +
        //            Ingrediente.id_Ingrediente.ToString() + ");";



        //        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //        cmd.ExecuteNonQuery();

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //// DE MOMENTO NO VAMOS A BORRAR NADA EN LA BD, LO DE ABAJO NO FUNCIONA
        //public bool BorrarIngrediente(int id)
        //{
        //    MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

        //    String sqlString = "SELECT * FROM Ingrediente WHERE codigo = " + id.ToString();

        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //    mySQLReader = cmd.ExecuteReader();
        //    if (mySQLReader.Read())
        //    {
        //        mySQLReader.Close();

        //        sqlString = "DELETE FROM Ingrediente WHERE codigo = " + id.ToString();
        //        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //        cmd.ExecuteNonQuery();

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


    }
}