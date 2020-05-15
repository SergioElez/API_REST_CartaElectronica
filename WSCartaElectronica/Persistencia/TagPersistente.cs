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
            p.nombre_ES = mySQLReader.GetString(1);
            p.nombre_EN = mySQLReader.GetString(2);
            p.color = mySQLReader.GetString(3);

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
        
        public ArrayList ObtenerTags(int idioma)
        {
            ArrayList arrayTags = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_Tag(" + idioma.ToString() + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                //EDITADO
                Tag p = LeerTag(mySQLReader);

                arrayTags.Add(p);
            }

            return arrayTags;
        }


        //public long GuardarTag(TagTraducido Tag)
        //{

        //    String sqlString = "CALL Crear_Tag('" +
        //        Tag.nombre_ES + "', '" +
        //        Tag.descripcion_ES + "', '" +
        //        Tag.nombre_EN + "', '" +
        //        Tag.descripcion_EN + "', '" +
        //        Tag.imagen + "', " +
        //        Tag.precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", "+
        //        Tag.id_Tag.ToString() + ");";

        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);
        //    Console.WriteLine(sqlString);
        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Error al insertar Tag: " + cmd.LastInsertedId);
        //    }


        //    long id = cmd.LastInsertedId;
        //    return id;
        //}

        //public bool ActualizarTag(int idioma, TagTraducido Tag)
        //{
        //    MySql.Data.MySqlClient.MySqlDataReader mySQLReader;


        //    String sqlString = "CALL Select_all_Tag(" + idioma + ");";


        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //    mySQLReader = cmd.ExecuteReader();
        //    if (mySQLReader.Read())
        //    {
        //        mySQLReader.Close();

        //         sqlString = "CALL Actualizar_Tag(" +
        //            Tag.id+ ", '" +
        //            Tag.nombre_ES + "', '" +
        //            Tag.descripcion_ES + "', '" +
        //            Tag.nombre_EN + "', '" +
        //            Tag.descripcion_EN + "', '" +
        //            Tag.imagen + "', " +
        //            Tag.precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", " +
        //            Tag.id_Tag.ToString() + ");";



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
        //public bool BorrarTag(int id)
        //{
        //    MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

        //    String sqlString = "SELECT * FROM Tag WHERE codigo = " + id.ToString();

        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //    mySQLReader = cmd.ExecuteReader();
        //    if (mySQLReader.Read())
        //    {
        //        mySQLReader.Close();

        //        sqlString = "DELETE FROM Tag WHERE codigo = " + id.ToString();
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

        public ArrayList BuscarTagPorEstablecimiento(int idioma, int establecimiento)
        {
            ArrayList arrayTags = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_Tag_establecimiento(" + idioma + ", " + establecimiento + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                //EDITADO
                Tag p = LeerTag(mySQLReader);

                arrayTags.Add(p);
            }
            return arrayTags;

        }

        public ArrayList BuscarTagsPorNombre(int idioma, int establecimiento, string busqueda)
        {
            ArrayList arrayTags = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_Tag_establecimiento_nombre(" + idioma + ", " + establecimiento + ", '%" + busqueda + "%');";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {

                //EDITADO
                Tag p = LeerTag(mySQLReader);

                arrayTags.Add(p);
            }

            return arrayTags;
        }

        //public ArrayList BuscarTagsPorTag(string tag)
        //{
        //    ArrayList arrayTags = new ArrayList();

        //    MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

        //    //String sqlString = "SELECT Trad.texto, Traduccion Trad WHERE Trad.id_tipo == 'Tag' AND Trad.idioma == 1";
        //    String sqlString = "SELECT P.id, Trad.texto P.imagen, P.precio, P.descripcion, P.id_Tag, Traduccion Trad, Tag P WHERE Trad.id_tipo == 'Tag' AND Trad.idioma == 1";
        //    //String sqlString = "SELECT * FROM Tag P, tag T, traduccion Trad WHERE Trad.id_tipo == 'Tag' AND Trad.idioma == 1 AND nombre LIKE '%" + tag + "%'";
        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

        //    mySQLReader = cmd.ExecuteReader();
        //    while (mySQLReader.Read())
        //    {
        //        Tag p = LeerTag(mySQLReader);

        //        arrayTags.Add(p);
        //    }

        //    return arrayTags;
        //}

    }
}