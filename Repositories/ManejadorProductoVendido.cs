using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWebAPI.Models
{
    public class ManejadorProductoVendido
    {
        static string cadenaDeConexion = "Data Source=IGNACIO-PC\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void InsertarProductoVendido(ProductoVendido productoVendido)
        {
            using (SqlConnection conection = new SqlConnection(cadenaDeConexion))
            {
                var query = @"INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) VALUES (@Stock, @IdProducto, @IdVenta)";

                SqlCommand comando = new SqlCommand(query, conection);

                comando.Parameters.AddWithValue("@Stock", productoVendido.Stock);
                comando.Parameters.AddWithValue("@IdProducto", productoVendido.IdProducto);
                comando.Parameters.AddWithValue("@IdVenta", productoVendido.IdVenta);

                conection.Open();
                comando.ExecuteNonQuery();

            }


        }

    }
}
