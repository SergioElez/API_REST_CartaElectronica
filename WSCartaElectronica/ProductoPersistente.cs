using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSCartaElectronica.Models;
using System.Collections;



namespace WSCartaElectronica
{
    public class ProductoPersistente
    {
        private MySql.Data.MySqlClient.MySqlConnection conexion;



        // ------ METODOS PARA CRUD ------ \\



        public ProductoPersistente()
        {
            string cadenaConexion = "server=127.0.0.1;uid=root;pwd=eslora;database=restaurante";
            //string cadenaConexion = "server=127.0.0.1;port=3309;uid=root;pwd=eslora;database=restaurante";

            try
            {
                conexion = new MySql.Data.MySqlClient.MySqlConnection();
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {

            }
        }
        public Producto LeerProducto(MySql.Data.MySqlClient.MySqlDataReader mySQLReader)
        {
            Producto p = new Producto();

            p.codigo = mySQLReader.GetInt32(0);
            p.nombre = mySQLReader.GetString(1);
            p.grupo = mySQLReader.GetString(2);
            p.especificaciones = mySQLReader.GetString(3);
            p.precio = mySQLReader.GetDouble(4);
            return p;
        }


        public Producto ObtenerProducto(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "SELECT * FROM producto WHERE codigo = " + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            //Si hay un error al hacer la consulta que devuelva null
            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    return LeerProducto(mySQLReader);
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
        
        public ArrayList ObtenerProductos()
        {
            ArrayList arrayProductos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "SELECT * FROM producto";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                Producto p = LeerProducto(mySQLReader);

                arrayProductos.Add(p);
            }

            return arrayProductos;
        }
        

        public long GuardarProducto(Producto productoAGuardar)
        {

            String sqlString = "INSERT INTO producto (codigo, nombre, grupo, especificaciones, precio) VALUES (" + 
                productoAGuardar.codigo + ",'" + 
                productoAGuardar.nombre + "','" + 
                productoAGuardar.grupo + "','" + 
                productoAGuardar.especificaciones + "', " + 
                productoAGuardar.precio + ");";

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);
            Console.WriteLine(sqlString);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Error al insertar producto: " + cmd.LastInsertedId);
            }


            long id = cmd.LastInsertedId;
            return id;
        }

        public bool ActualizaProducto(int id, Producto productoAActualizar)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "SELECT * FROM producto WHERE codigo = " + id.ToString();

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read())
            {
                mySQLReader.Close();

                sqlString = "UPDATE producto SET " +
                    "codigo = " + productoAActualizar.codigo + ", " +
                    "nombre = '" + productoAActualizar.nombre + "', " +
                    "grupo = '" + productoAActualizar.grupo + "', " +
                    "especificaciones = '" + productoAActualizar.especificaciones + "', " +
                    "precio = " + productoAActualizar.precio + " " +
                    "WHERE codigo = " + productoAActualizar.codigo + ";";



                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

                cmd.ExecuteNonQuery();

                return true;
            }
            else
            {
                return false;
            }
        }


        public bool BorrarProducto(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "SELECT * FROM producto WHERE codigo = " + id.ToString();

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read())
            {
                mySQLReader.Close();

                sqlString = "DELETE FROM producto WHERE codigo = " + id.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

                cmd.ExecuteNonQuery();
                
                return true;
            }
            else
            {
                return false;
            }
        }


        // ------ METODOS EXTRA ----- \\

        public ArrayList BuscarProductosPorNombre(string nombre)
        {
            ArrayList arrayProductos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "SELECT * FROM producto WHERE nombre LIKE '%" + nombre + "%'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                Producto p = LeerProducto(mySQLReader);

                arrayProductos.Add(p);
            }

            return arrayProductos;
        }

        public ArrayList BuscarProductosPorTag(string tag)
        {
            ArrayList arrayProductos = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            //String sqlString = "SELECT Trad.texto, Traduccion Trad WHERE Trad.id_tipo == 'plato' AND Trad.idioma == 1";
            String sqlString = "SELECT P.id, Trad.texto P.imagen, P.precio, P.descripcion, P.id_familia, Traduccion Trad, Plato P WHERE Trad.id_tipo == 'plato' AND Trad.idioma == 1";
            //String sqlString = "SELECT * FROM plato P, tag T, traduccion Trad WHERE Trad.id_tipo == 'plato' AND Trad.idioma == 1 AND nombre LIKE '%" + tag + "%'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                Producto p = LeerProducto(mySQLReader);

                arrayProductos.Add(p);
            }

            return arrayProductos;
        }

    }
}