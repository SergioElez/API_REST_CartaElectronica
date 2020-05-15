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
                return null;
            }
        }
        
        public ArrayList ObtenerFamilias(int idioma)
        {
            ArrayList arrayFamilias = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_Familia(" + idioma.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                Familia p = new Familia();

                p.id = mySQLReader.GetInt32(0);
                p.nombre = mySQLReader.GetString(1);
                p.imagen = mySQLReader.GetString(2);

                arrayFamilias.Add(p);
            }

            return arrayFamilias;
        }


        //public long GuardarFamilia(FamiliaTraducido Familia)
        //{

        //    String sqlString = "CALL Crear_Familia('" +
        //        Familia.nombre_ES + "', '" +
        //        Familia.descripcion_ES + "', '" +
        //        Familia.nombre_EN + "', '" +
        //        Familia.descripcion_EN + "', '" +
        //        Familia.imagen + "', " +
        //        Familia.precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", "+
        //        Familia.id_familia.ToString() + ");";

        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);
        //    Console.WriteLine(sqlString);
        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Error al insertar Familia: " + cmd.LastInsertedId);
        //    }


        //    long id = cmd.LastInsertedId;
        //    return id;
        //}

        //public bool ActualizarFamilia(int idioma, FamiliaTraducido Familia)
        //{
        //    MySql.Data.MySqlClient.MySqlDataReader mySQLReader;


        //    String sqlString = "CALL Select_all_Familia(" + idioma + ");";


        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //    mySQLReader = cmd.ExecuteReader();
        //    if (mySQLReader.Read())
        //    {
        //        mySQLReader.Close();

        //         sqlString = "CALL Actualizar_Familia(" +
        //            Familia.id+ ", '" +
        //            Familia.nombre_ES + "', '" +
        //            Familia.descripcion_ES + "', '" +
        //            Familia.nombre_EN + "', '" +
        //            Familia.descripcion_EN + "', '" +
        //            Familia.imagen + "', " +
        //            Familia.precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", " +
        //            Familia.id_familia.ToString() + ");";



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
        //public bool BorrarFamilia(int id)
        //{
        //    MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

        //    String sqlString = "SELECT * FROM Familia WHERE codigo = " + id.ToString();

        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //    mySQLReader = cmd.ExecuteReader();
        //    if (mySQLReader.Read())
        //    {
        //        mySQLReader.Close();

        //        sqlString = "DELETE FROM Familia WHERE codigo = " + id.ToString();
        //        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //        cmd.ExecuteNonQuery();

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        //// ------ METODOS EXTRA ----- \\

        public ArrayList BuscarFamiliaPorEstablecimiento(int idioma, int establecimiento)
        {
            ArrayList arrayFamilias = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_familia_establecimiento(" + idioma + ", " + establecimiento + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                Familia p = new Familia();

                p.id = mySQLReader.GetInt32(0);
                p.nombre = mySQLReader.GetString(1);
                p.imagen = mySQLReader.GetString(2);

                arrayFamilias.Add(p);
            }
            return arrayFamilias;

        }

        public ArrayList BuscarFamiliasPorNombre(int idioma, int establecimiento, string busqueda)
        {
            ArrayList arrayFamilias = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_familia_establecimiento_nombre(" + idioma + ", " + establecimiento + ", '%" + busqueda + "%');";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                Familia p = new Familia();

                p.id = mySQLReader.GetInt32(0);
                p.nombre = mySQLReader.GetString(1);
                p.imagen = mySQLReader.GetString(2);

                arrayFamilias.Add(p);
            }

            return arrayFamilias;
        }

        //public ArrayList BuscarFamiliasPorTag(string tag)
        //{
        //    ArrayList arrayFamilias = new ArrayList();

        //    MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

        //    //String sqlString = "SELECT Trad.texto, Traduccion Trad WHERE Trad.id_tipo == 'Familia' AND Trad.idioma == 1";
        //    String sqlString = "SELECT P.id, Trad.texto P.imagen, P.precio, P.descripcion, P.id_familia, Traduccion Trad, Familia P WHERE Trad.id_tipo == 'Familia' AND Trad.idioma == 1";
        //    //String sqlString = "SELECT * FROM Familia P, tag T, traduccion Trad WHERE Trad.id_tipo == 'Familia' AND Trad.idioma == 1 AND nombre LIKE '%" + tag + "%'";
        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //    mySQLReader = cmd.ExecuteReader();
        //    while (mySQLReader.Read())
        //    {
        //        Familia p = LeerFamilia(mySQLReader);

        //        arrayFamilias.Add(p);
        //    }

        //    return arrayFamilias;
        //}

    }
}