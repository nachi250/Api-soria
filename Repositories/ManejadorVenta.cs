using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWebAPI.Models
{
    internal static class ManejadorVenta
    {
        public static List<Venta> ObtenerVentas(long idUsuario)
        {
            List<Venta> ventas = new List<Venta>();
            string cadenaConexion = "Data Source=IGNACIO-PC\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection conn = new SqlConnection(cadenaConexion)) 
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Venta WHERE @IdUsuario = idUsuario", conn);
                comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Venta ventaTemp = new Venta();
                        ventaTemp.Id = reader.GetInt64(0);
                        ventaTemp.Comentarios = reader.GetString(1);
                        ventaTemp.IdUsuario = reader.GetInt64(2);
                        ventas.Add(ventaTemp);
                    }
                }

                return ventas;

            }
     
        }
    }
}
