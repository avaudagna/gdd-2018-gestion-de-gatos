﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Windows.Forms;
using PalcoNet.Repositorios;
using PalcoNet.Modelo;


namespace PalcoNet.Repositorios
{
    class EmpresasRepositorio
    {  
        public static List<SqlParameter> GenerarParametrosEmpresa(Empresa empresa,string username)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@dni", empresa.Cuit));
            parametros.Add(new SqlParameter("@apellido", empresa.RazonSocial));
            parametros.Add(new SqlParameter("@mail", empresa.Email));
            parametros.Add(new SqlParameter("@telefono", empresa.Telefono));
            parametros.Add(new SqlParameter("@localidad", empresa.Domicilio.Localidad));
            parametros.Add(new SqlParameter("@username", username));
            return parametros;
        }
        public static void CreateEmpresa(Empresa empresa,string username)
        {
            List<SqlParameter> parametros = GenerarParametrosEmpresa(empresa,username);
            DataBase.WriteInBase("Ingresarempresas", "SP", parametros);

        }

        
        public static void UpdateEmpresa(Empresa empresa,string username)
        {
            List<SqlParameter> parametros = GenerarParametrosEmpresa(empresa,username);
            DataBase.WriteInBase("Updateempresa", "SP", parametros);

        }

        
        public static void DeleteEmpresa(Empresa empresa,string username)
        {
            List<SqlParameter> parametros = GenerarParametrosEmpresa(empresa,username);
            DataBase.WriteInBase("Deleteempresa", "SP", parametros);

        }
        
        public static Empresa ReadEmpresaFromDb(SqlDataReader reader)
        {
            return new Empresa()
                         {
                        RazonSocial=reader.GetValue(Ordinales.Empresa["razonSocial"]).ToString(),
                        Cuit=reader.GetValue(Ordinales.Empresa["cuit"]).ToString(),
                        Email = reader.GetValue(Ordinales.Empresa["email"]).ToString(),
                        Telefono = reader.GetValue(Ordinales.Empresa["telefono"]).ToString(),
                        Domicilio = DireccionRepositorio.ReadDireccionFromDb(reader.GetValue(Ordinales.Empresa["cuit"]).ToString())
                         };
        }
       
        public static List<Empresa> GetempresaByRazonSocial(string unaRazon)
        {
            var empresas = new List<Empresa>();
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Razon", unaRazon));
            var query = DataBase.ejecutarFuncion("Select * from empresa e where e.empr_razon like('@Razon%')", parametros);
            SqlDataReader reader = query.ExecuteReader();
            while (reader.Read())
            {
                empresas.Add(
                    ReadEmpresaFromDb(reader));
            }
            reader.Close();
            return empresas;
        }
        public static List<Empresa> GetempresaByCuit(string unNumero)
        {
            var empresas = new List<Empresa>();
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@numero", unNumero));
            var query = DataBase.ejecutarFuncion("Select * from empresa e where e.empr_cuit like('@numero%')", parametros);
            SqlDataReader reader = query.ExecuteReader();
            while (reader.Read())
            {
                empresas.Add(
                    ReadEmpresaFromDb(reader));
            }
            reader.Close();
            return empresas;
        }
        public static List<Empresa> GetEmpresasByEmail(string unEmail)
        {
            var empresas = new List<Empresa>();
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@email", unEmail));
            var query = DataBase.ejecutarFuncion("Select * from empresa e where e.empr_email like('@email%')", parametros);
            SqlDataReader reader = query.ExecuteReader();
            while (reader.Read())
            {
                empresas.Add(
                    ReadEmpresaFromDb(reader));
            }
            reader.Close();
           
            return empresas;
        }

        public static bool verificaConformacionCuit()
        {
            return true;
        }


        internal static void deshabilitar(string empresa_cuit,string username)
        {
            List<SqlParameter> parametros = DataBase.GenerarParametrosDeleteFromString(empresa_cuit, username);
            DataBase.WriteInBase("[dbo].[eliminarEmpresa]", "SP", parametros);
        }

        internal static Empresa getEmpresa(string cuit)
        {
            throw new NotImplementedException();
        }

        internal static void actualizar(Empresa empresa)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@razon_social", empresa.RazonSocial));
            parametros.Add(new SqlParameter("@cuit", empresa.Cuit));
            parametros.Add(new SqlParameter("@mail", empresa.Email));
            parametros.Add(new SqlParameter("@telefono", empresa.Telefono));
            parametros.Add(new SqlParameter("@domicilio_id", empresa.Domicilio.Id));
            parametros.Add(new SqlParameter("@habilitada", empresa.Habilitada ? 1 : 0));
            DataBase.ejecutarSP("[dbo].[sp_actualizar_empresa]", parametros);
        }

        internal static void agregar(Empresa empresa,string contraseña)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@razon_social", empresa.RazonSocial));
            parametros.Add(new SqlParameter("@cuit", empresa.Cuit));
            parametros.Add(new SqlParameter("@mail", empresa.Email));
            parametros.Add(new SqlParameter("@telefono", empresa.Telefono));
            parametros.Add(new SqlParameter("@dom_id", empresa.Domicilio.Id));
            parametros.Add(new SqlParameter("@contraseña",contraseña));
            parametros.Add(new SqlParameter("@fecha_creacion", DateTime.Now));
            DataBase.ejecutarSP("[dbo].[sp_crear_empresa]", parametros);
        }

        public static List<Empresa> getEmpresas(string razonSocial, string mail, string cuit)
        {
            StringBuilder stringBuilder = new StringBuilder();
            String sql = stringBuilder
                .Append("SELECT * FROM GESTION_DE_GATOS.Empresas WHERE ")
                .Append("Emp_Razon_Social LIKE ('%" + razonSocial + "%') AND ")
                .Append("Emp_Mail LIKE ('%" + mail + "%') AND ")
                .Append("Emp_Cuit LIKE ('%" + cuit + "%')")
                .ToString();
            List<Empresa> empresas = new List<Empresa>();
            SqlDataReader lector = DataBase.GetDataReader(sql, "T", new List<SqlParameter>());
            while (lector.Read())
            {
               empresas.Add(Empresa.buildEmpresa(lector));
            }
            lector.Close();
            return empresas;
        }

        public static void eliminar(Empresa empresa)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@cuit", empresa.Cuit));
            DataBase.ejecutarSP("[dbo].[sp_eliminar_empresa]", parametros);
        }

        public static bool cambiarEstado(Empresa empresa)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            SqlParameter output = new SqlParameter("@resultado", 0);
            output.Direction = ParameterDirection.Output;
            parametros.Add(output);
            parametros.Add(new SqlParameter("@cuit", empresa.Cuit));
            parametros.Add(new SqlParameter("@estado_final", empresa.Habilitada ? 0 : 1));
            SqlCommand sqlCommand = DataBase.ejecutarSP("[dbo].[sp_cambiar_estado_empresa]", parametros);
            return Convert.ToInt32(sqlCommand.Parameters["@resultado"].Value) == 1;
        }
    }
}



