﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalcoNet.Modelo
{
   public static class Ordinales
    {

       public static Dictionary<string, int> camposUsuario = new Dictionary<string, int>
                                                {
                                                    {"id", 0},
                                                    {"username", 1},
                                                    {"estado", 2},
                                                };
       public static Dictionary<string, int> camposRol = new Dictionary<string, int>
                                                {
                                                    {"id", 0},
                                                    {"nombre", 1}
                                                };
       public static Dictionary<string, int> camposGetClientes = new Dictionary<string, int>
                                                {
                                                    {"tipo_doc_descr", 0},
                                                    {"cli_doc", 1},
                                                    {"cli_cuil", 2},
                                                    {"cli_nombre", 3},
                                                    {"cli_apellido", 4},
                                                    {"cli_mail", 5},
                                                    {"dom_calle", 6},
                                                    {"dom_nro_calle", 7},
                                                    {"dom_depto",8},
                                                    {"dom_piso",9},
                                                    {"dom_localidad", 10},
                                                    {"dom_cod_postal", 11}
                                                };
       public static Dictionary<string, int> camposFuncionalidad = new Dictionary<string, int>
                                                {
                                                    {"id", 0},
                                                    {"descripcion", 1}
                                                };
       public static Dictionary<string, int> Cliente = new Dictionary<string, int>
                                                {   
                                                    {"nombre",0},
                                                    {"apellido",1},
                                                    {"tipoDocumento",2},
                                                    {"numeroDocumento",3},
                                                    {"cuil",4},
                                                    {"email",5},
                                                    {"telefono",6},
                                                    {"fechaNacimiento",7},
                                                    {"fechaCreacion",8},
                                                    

                                                };
       public static Dictionary<string, int> Direccion = new Dictionary<string, int> 
                                                 {
                                                 {"codigo",0}, 
                                                 {"numero",5},
                                                 {"departamento",4},
                                                 {"localidad",2},  
                                                 {"codPostal",1},
                                                 {"calle",6}
                                                 ,{"piso",3}
                                                                                               
                                                 };

       public static Dictionary<string, int> Tarjeta = new Dictionary<string, int> 
                                                    {
                                                    {"numero",0},
                                                    {"nombre",1},
                                                    {"fechaVencimiento",2},
                                                    {"ccv",3}
                                                    };
       public static Dictionary<string, int> Empresa = new Dictionary<string, int> 
                                                    {
                                                    {"razonSocial",0}, 
                                                    {"email",1},
                                                    {"telefono",2},
                                                    {"codPostal",3}, 
                                                    {"ciudad",4},
                                                    {"cuit  ",5}
                                                    
                                                    };
       public static Dictionary<string, int> Rubro = new Dictionary<string, int> 
                                                    {
                                                    {"codigo",0},
                                                    {"descripcion",1}
                                                    
                                                    };
       public static Dictionary<string, int> Publicacion = new Dictionary<string, int> 
                                                    {
                                                    {"codigo",0},
                                                    {"descripcion",1},
                                                    {"fechaPublicacion",2}
                                                    };
       public static Dictionary<string, int> Ubicacion = new Dictionary<string, int> 
                                                    {
                                                    {"fila",0},
                                                    {"asiento",1},
                                                    {"tipo",3}
                                                    };
       public static Dictionary<string, int> Grado = new Dictionary<string, int> 
                                                    {
                                                    {"tipo",0},
                                                    {"comision",1},
                                                    {"descuento",3}
                                                    };
       public static Dictionary<string, int> EstadoPublicacion = new Dictionary<string, int> 
                                                    {
                                                    {"id",0},
                                                    {"descripcion",1 },
                                                    {"puedeCambiarDeEstado",2},
                                                    };
       public static Dictionary<string, int> Espectaculo = new Dictionary<string, int> 
                                                    {
                                                    {"id",0},
                                                    {"descripcion",1 },
                                                    {"fecha",2},
                                                    {"hora",3},
                                                    {"fechaVencimiento",4},
                                                    {"idRubro",5},
                                                    {"idEmpresa",6},
                                                    {"idDomicilio",7}
                                                    }; 
   }

}
