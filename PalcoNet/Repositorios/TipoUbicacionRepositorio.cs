﻿using PalcoNet.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalcoNet.Repositorios
{
    class TipoUbicacionRepositorio
    {
        public static List<SqlParameter> GenerarParametrosTipoUbicacion(TipoUbicacion tipoUbicacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@Descripcion", tipoUbicacion.Descripcion));
            return parametros;
        }
        
        public static void CreateTipoUbicacion(TipoUbicacion tipoUbicacion)
        {
            List<SqlParameter> parametros = GenerarParametrosTipoUbicacion(tipoUbicacion);
            DataBase.WriteInBase("IngresartipoUbicacions", "SP", parametros);

        }


        public static void UpdateTipoUbicacion(TipoUbicacion tipoUbicacion)
        {
            List<SqlParameter> parametros = GenerarParametrosTipoUbicacion(tipoUbicacion);
            DataBase.WriteInBase("UpdatetipoUbicacion", "SP", parametros);

        }


        public static void DeleteTipoUbicacion(int id,string username)
        {
            List<SqlParameter> parametros = DataBase.GenerarParametrosDeleteFromInt(id,username);
            DataBase.WriteInBase("DeletetipoUbicacion", "SP", parametros);
        }

        public static TipoUbicacion ReadTipoUbicacionFromDb(int id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id", id));
            SqlCommand query = DataBase.ejecutarFuncion("SELECT * from GESTION_DE_GATOS.Ubicaciones_Tipo t " +
                                                 "WHERE t.Ubic_Tipo_Cod = @id", parametros);
            SqlDataReader lector = query.ExecuteReader();
            if (lector.Read())
            {
                return TipoUbicacion.build(lector);

            }
            return null;

        }
    }
}
