﻿using PalcoNet.Modelo;
using PalcoNet.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PalcoNet.Repositorios
{
    class RolRepositorio
    {

        public static List<Funcionalidad> buscarFuncionalidadesPorRol(Rol rol)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@rol_id", rol.id));
            SqlDataReader lector = DataBase.GetDataReader("[dbo].[sp_funcionalidades_por_rol]", "SP", parametros);
            List<Funcionalidad> funcionalidades = new List<Funcionalidad>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    funcionalidades.Add(Funcionalidad.buildFuncionalidad(lector));
                }
            }
            lector.Close();
            return funcionalidades;
        }

        public static bool esRolExistente(String nombre)
        {
            SqlDataReader lector = buscarFilasRol(nombre);
            bool esRolExistente = lector.HasRows;
            lector.Close();
            return esRolExistente;
        }

        public static SqlDataReader buscarFilasRol(String nombre)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombre", nombre));
            return DataBase.GetDataReader("404_NOT_FOUND.SP_BUSCAR_ROL", "SP", parametros);
        }

        public static void agregar(Rol rol, List<Funcionalidad> funcionalidades)
        {
            List<SqlParameter> parametros_rol = new List<SqlParameter>();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombre", rol.nombre));
            SqlParameter output = new SqlParameter("@id", -1);
            output.Direction = ParameterDirection.Output;
            parametros.Add(output);
            SqlCommand sqlCommand = DataBase.ejecutarSP("[dbo].[sp_crear_rol]", parametros);
            int idRol = Convert.ToInt32(sqlCommand.Parameters["@id"].Value);
            foreach (Funcionalidad fun in funcionalidades)
            {
                DataBase.GetDataReader("INSERT INTO GESTION_DE_GATOS.Funcionalidad_Por_Rol (Rol_Id,Func_Id) VALUES("+idRol+","+fun.id+")", "T", new List<SqlParameter>());
            }
        }

        public static List<Rol> getRoles(int modo=0)
        {
            List<Rol> roles = new List<Rol>();
            StringBuilder stringBuilder = new StringBuilder();
            String sql = stringBuilder
                .Append("SELECT * FROM GESTION_DE_GATOS.ROLES WHERE (Rol_Nombre = 'CLIENTE' or Rol_nombre = 'EMPRESA') and Rol_Estado=1")
                .ToString();
            if (modo == 1) sql = sql.Replace(" WHERE (Rol_Nombre = 'CLIENTE' or Rol_nombre = 'EMPRESA') and Rol_Estado=1", "");
            SqlDataReader lector = DataBase.GetDataReader(sql, "T", new List<SqlParameter>());
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    Rol rol = modo == 0 ? Rol.buildRol(lector) : Rol.buildRolListado(lector);
                    roles.Add(rol);
                }
            }
            lector.Close();
            return roles;
        }

        public static List<Funcionalidad> getFuncionalidades(Rol rol)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            List<Funcionalidad> funcionalidades = new List<Funcionalidad>();
            parametros.Add(new SqlParameter("@rol_id", DBNull.Value));
            SqlDataReader lector = DataBase.GetDataReader("dbo.sp_funcionalidades_por_rol", "SP", parametros);
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    funcionalidades.Add(Funcionalidad.buildFuncionalidad(lector));
                }
            }
            lector.Close();
            return funcionalidades;
        }


        public static void deshabilitar(String nombre)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombre", nombre));
            DataBase.WriteInBase("404_NOT_FOUND.SP_DESHABILITAR_ROL", "SP", parametros);
        }

        public static void modificarRol(Rol rol, List<String> funcionalidades)
        {
            List<SqlParameter> parametros_rol = new List<SqlParameter>();
            List<SqlParameter> parametros_funcionalidades_rol = new List<SqlParameter>();
            parametros_rol.Add(new SqlParameter("@nombre", rol.nombre));
            parametros_rol.Add(new SqlParameter("@habilitado", rol.Habilitado));
            DataBase.WriteInBase("404_NOT_FOUND.SP_MODIFICAR_ROL", "SP", parametros_rol);
            foreach (String funcionalidad in funcionalidades)
            {
                parametros_funcionalidades_rol.Add(new SqlParameter("@rol", rol.nombre));
                parametros_funcionalidades_rol.Add(new SqlParameter("@funcionalidad", funcionalidad));
                DataBase.WriteInBase("404_NOT_FOUND.SP_AGREGAR_ROL_FUNCIONALIDAD", "SP", parametros_funcionalidades_rol);
                parametros_funcionalidades_rol.Clear();
            }
        }

        public static bool tieneFuncionalidad(int rol_nombre, String funcionalidad)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@rol_nombre", rol_nombre));
            parametros.Add(new SqlParameter("@funcionalidad", funcionalidad));
            SqlDataReader lector = DataBase.GetDataReader("404_NOT_FOUND.SP_TIENE_FUNCIONALIDAD", "SP", parametros);
            bool tieneFuncionalidad = lector.HasRows && lector.Read();
            lector.Close();
            return tieneFuncionalidad;
        }
        public static List<Rol> getRoles(string descripcion)
        {
            List<Rol> roles = new List<Rol>();
            SqlDataReader lector = DataBase.GetDataReader("SELECT * FROM GESTION_DE_GATOS.Roles WHERE Rol_Nombre LIKE ('%" + descripcion + "%')", "T", new List<SqlParameter>());
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    Rol rol =Rol.buildRolListado(lector);
                    roles.Add(rol);
                }
            }
            lector.Close();
            return roles;
        }
    }
}
