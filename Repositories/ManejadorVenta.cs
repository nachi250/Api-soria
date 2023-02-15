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
        static string cadenaDeConexion = "Data Source=IGNACIO-PC\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<Venta> ObtenerVentas(long idUsuario)
        {
            List<Venta> ventas = new List<Venta>();
            
            using (SqlConnection conection = new SqlConnection(cadenaDeConexion)) 
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Venta WHERE @IdUsuario = idUsuario", conection);
                comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                conection.Open();

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

        public static long InsertarVenta(Venta venta)
        {

            using (SqlConnection conection = new SqlConnection(cadenaDeConexion))
            {
                var query = "INSERT INTO Venta (Comentarios, IdUsuario) VALUES(@Comentarios, @IdUsuario); SELECT @@IDENTITY";
                SqlCommand comando = new SqlCommand(query, conection);

                comando.Parameters.AddWithValue("@Comentarios", venta.Comentarios);
                comando.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);

                conection.Open();

                return Convert.ToInt64(comando.ExecuteScalar());
            }
        }

        public static void CargarVenta(long idUsuario, List<Producto> productosVendidos)
        {
            Venta venta = new Venta();
            using (SqlConnection conection = new SqlConnection(cadenaDeConexion)) 
            {
                SqlCommand comando = new SqlCommand();
                conection.Open();

                venta.Comentarios = "Prueba Venta desde Postman";
                venta.IdUsuario = idUsuario;
                venta.Id = InsertarVenta(venta);

                foreach (Producto producto in productosVendidos)
                {
                    ProductoVendido productoVendido = new ProductoVendido();
                    productoVendido.Stock = producto.Stock;
                    productoVendido.IdProducto = producto.Id;
                    productoVendido.IdVenta = venta.Id;

                    ManejadorProductoVendido.InsertarProductoVendido(productoVendido);

                    ManejadorProducto.UpdateStockProducto(productoVendido.IdProducto, productoVendido.Stock);

                }

            }
        }


    }
}
