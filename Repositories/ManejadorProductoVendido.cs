using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWebAPI.Models
{
    public static class ManejadorProductoVendido
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


        public static List<Producto> ObtenerProductosVendidosId(long idUsuario)
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection conection = new SqlConnection(cadenaDeConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT *\r\nFROM Producto\r\ninner join ProductoVendido\r\nON Producto.Id = ProductoVendido.IdProducto\r\ninner join Venta\r\nON Venta.Id = ProductoVendido.IdVenta\r\nWHERE Venta.IdUsuario = @IdUsuario", conection);

                comando.Parameters.AddWithValue("@IdUsuario", idUsuario);

                conection.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto.Id = reader.GetInt64(0);
                        producto.Descripciones = reader.GetString(1);
                        producto.Costo = reader.GetDecimal(2);
                        producto.PrecioVenta = reader.GetDecimal(3);
                        producto.Stock = reader.GetInt32(4);
                        producto.IdUsuario = reader.GetInt64(5);
                        productos.Add(producto);
                    }
                }
                return productos;
            }
        }
        


    }
}
