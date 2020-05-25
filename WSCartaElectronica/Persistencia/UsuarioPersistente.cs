﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSCartaElectronica.Models;
using System.Collections;



namespace WSCartaElectronica
{
    public class UsuarioPersistente
    {
        private MySql.Data.MySqlClient.MySqlConnection conexion;



        // ------ METODOS PARA CRUD ------ \\



        public UsuarioPersistente()
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
        public Usuario LeerUsuario(MySql.Data.MySqlClient.MySqlDataReader mySQLReader)
        {
            Usuario p = new Usuario();

            p.id = mySQLReader.GetInt32(0);
            p.nombre = mySQLReader.GetString(1);
            p.correo = mySQLReader.GetString(2);
            p.contraseña = mySQLReader.GetString(3);
            p.imagen = mySQLReader.GetString(4);
            p.puntos= mySQLReader.GetInt32(5);
            p.id_empresa= mySQLReader.GetInt32(6);

            return p;
        }


        public Usuario ObtenerUsuario(int empresa, int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_usuario_empresa_id(" + empresa + ", " + id + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    return LeerUsuario(mySQLReader);
                }
                else
                {
                    conexion.Close();
                    return null;
                }
            }
            catch(Exception e)
            {
                conexion.Close();
                return null;
            }
            
        }
        
        public ArrayList ObtenerUsuarios(int empresa)
        {
            ArrayList arrayUsuarios = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_usuario_empresa(" + empresa + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);



            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    Usuario p = LeerUsuario(mySQLReader);

                    arrayUsuarios.Add(p);
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
            
            
            return arrayUsuarios;

        }


        public long GuardarUsuario(Usuario usuario)
        {

            String sqlString = "CALL Crear_Usuario('" +
                usuario.nombre + "', '" +
                usuario.correo+ "', '" +
                usuario.contraseña+ "', '" +
                usuario.imagen + "', " +
                usuario.puntos.ToString() + ", " +
                usuario.id_empresa.ToString() + ");";

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);
            Console.WriteLine(sqlString);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Error al insertar Usuario: " + cmd.LastInsertedId);
            }


            long id = cmd.LastInsertedId;
            return id;
        }


        //// ------ METODOS EXTRA ----- \\

        public ArrayList BuscarUsuariosPorEmpresaYCorreo(int empresa, string correo)
        {
            ArrayList arrayUsuarios = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_usuario_empresa_correo(" + empresa + ", '" + correo + "');";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                Usuario p = LeerUsuario(mySQLReader);

                arrayUsuarios.Add(p);
            }

            return arrayUsuarios;
        }

        public bool IniciarSesion(string correo, string contraseña)
        {
            ArrayList arrayUsuarios = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Select_all_usuario_correo_contraseña('" + correo + "', '" + contraseña + "');";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                Usuario p = LeerUsuario(mySQLReader);

                arrayUsuarios.Add(p);
            }

            if(arrayUsuarios.Count == 0)
            {
                return false;
            }
            else if (arrayUsuarios.Count == 1)
            {
                return true;
            }
            else
            {
                Console.WriteLine("ERROR: Existen dos o mas usuarios con el mismo correo y contraseña");
                return false;
            }
        }

        public bool ComprobarCorreo(string _correo)
        {
            string correo = "";

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "Select correo from usuario where correo = '" + _correo + "';";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                correo = mySQLReader.GetString(0);
            }

            if (correo == _correo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Usuario ObtenerUsuarioPorCorreo(string _correo)
        {

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "Select id, nombre, correo, contraseña, imagen, puntos, id_empresa from usuario where correo = '" + _correo + "';";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    return LeerUsuario(mySQLReader);
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


        public bool ActivarMoneda(int id_usuario)
        {

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader;

            String sqlString = "CALL Activar_moneda(" + id_usuario + ");";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conexion);

            try
            {
                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    return mySQLReader.GetBoolean(0);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}