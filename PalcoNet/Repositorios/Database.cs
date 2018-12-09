﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace PalcoNet.Repositorios
{
    static class DataBase
    {
        private static SqlConnection connection = new SqlConnection();

        public static SqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.ConnectionString = @System.Configuration.ConfigurationManager.ConnectionStrings["GddDB"].ConnectionString;
                connection.Open();
            }
            return connection;
        }


        public static SqlCommand BuildSQLCommand(String commandtext, List<SqlParameter> parameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = commandtext;
            sqlCommand.Connection = GetConnection();
            if (parameters != null)
            {
                foreach (SqlParameter param in parameters) { sqlCommand.Parameters.Add(param); }
            }
            return sqlCommand;
        }

        public static SqlCommand ejecutarSP(String nombreSP, List<SqlParameter> parametros)
        {
            SqlCommand sqlCommand = DataBase.BuildSQLCommand(nombreSP, parametros);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.ExecuteNonQuery();
            return sqlCommand;
        }
        public static SqlCommand ejecutarFuncion(string funcionSql, List<SqlParameter> parametros)
        {
            SqlCommand sqlCommand = DataBase.BuildSQLCommand(funcionSql, parametros);
            sqlCommand.CommandType = CommandType.Text;
            return sqlCommand;
        }

        public static SqlDataReader GetDataReader(String commandtext, String commandtype,
                                                    List<SqlParameter> parameters)
        {
            SqlCommand sqlCommand = BuildSQLCommand(commandtext, parameters);
            SetCommandType(commandtype, sqlCommand);
            return sqlCommand.ExecuteReader();
        }

        private static void SetCommandType(String commandtype, SqlCommand sqlCommand)
        {
            switch (commandtype)
            {
                case "T":
                    sqlCommand.CommandType = CommandType.Text;
                    break;
                case "TD":
                    sqlCommand.CommandType = CommandType.TableDirect;
                    break;
                case "SP":
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    break;
            }
        }

        public static int WriteInBase(String commandtext, String commandtype, List<SqlParameter> parameters)
        {
            SqlCommand sqlCommand = BuildSQLCommand(commandtext, parameters);
            SetCommandType(commandtype, sqlCommand);
            return sqlCommand.ExecuteNonQuery();

        }
        public static List<SqlParameter> GenerarParametrosDelete(int id, string username)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@Id", id));
            parametros.Add(new SqlParameter("@Username", username));
            return parametros;
        }
    }
}
