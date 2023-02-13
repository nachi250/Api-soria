using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWebAPI.Models
{
    internal static class ManejadorProducto
    {
        static string cadenaDeConexion = "Data Source=IGNACIO-PC\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<Producto> ObtenerProductos(long idUsuario)
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection conn = new SqlConnection(cadenaDeConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE @IdUsuario = idUsuario", conn);
                comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto productoTemporal = new Producto();
                        productoTemporal.Id = reader.GetInt64(0);
                        productoTemporal.Descripciones = reader.GetString(1);
                        productoTemporal.Costo = reader.GetDecimal(2);
                        productoTemporal.PrecioVenta = reader.GetDecimal(3);
                        productoTemporal.Stock = reader.GetInt32(4);
                        productoTemporal.IdUsuario = reader.GetInt64(5);

                        productos.Add(productoTemporal);
                    }
                }

                return productos;

            }
        }

        public static Producto ObtenerProducto(long id)
        {
            Producto producto = new Producto();

            using (SqlConnection conn = new SqlConnection(cadenaDeConexion))
            {

                SqlCommand comando2 = new SqlCommand("SELECT * FROM Producto WHERE @Id=id", conn);
                comando2.Parameters.AddWithValue("@Id", id);

                conn.Open();

                SqlDataReader reader = comando2.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    Producto productoTemporal = new Producto();
                    producto.Id = reader.GetInt64(0);
                    producto.Descripciones = reader.GetString(1);
                    producto.Costo = reader.GetDecimal(2);
                    producto.PrecioVenta = reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);

                }
                return producto;
            }
        }




        public static List <Producto> ObtenerProductoVendido(long idUsuario)
        {
            List<long> ListaIdProductos = new List<long>();

            using (SqlConnection conn = new SqlConnection(cadenaDeConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT IdProducto FROM Venta\r\ninner join ProductoVendido\r\non venta.id = ProductoVendido.IdVenta\r\nwhere IdUsuario = @idUsuario", conn);

                comando.Parameters.AddWithValue("@idUsuario", idUsuario);

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ListaIdProductos.Add(reader.GetInt64(0));
                    }
                }
            }
            List<Producto> productos = new List<Producto>();
            foreach (var id in ListaIdProductos)
            {
                Producto prodTemp = ObtenerProducto(id);
                productos.Add(prodTemp);
            }

            return productos;

        }



        public static Producto ActualizarProducto(Producto producto)
        {

            using (SqlConnection conection = new SqlConnection(cadenaDeConexion))

            {
                SqlCommand comando = new SqlCommand();

                comando.Connection = conection;
                comando.Connection.Open();
                comando.CommandText = @"UPDATE Producto
                                        SET [Descripciones]= @Descripciones,
                                            [Costo]= @Costo, 
                                            [PrecioVenta]= @PrecioVenta, 
                                            [Stock]=@Stock
                                        WHERE [Id]=@Id";


                comando.Parameters.AddWithValue("@Descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@Costo", producto.Costo);
                comando.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@Stock", producto.Stock);
                comando.Parameters.AddWithValue("@Id", producto.Id);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }

            return producto;
        }



        public static int DeleteProducto(long id)
        {
            
            using (SqlConnection conection = new SqlConnection(cadenaDeConexion)) 
            {
                SqlCommand comando = new SqlCommand("DELETE FROM Producto WHERE Id=@Id", conection);
                comando.Parameters.AddWithValue("@Id", id);
                conection.Open();
                return comando.ExecuteNonQuery();
            }
        }


        public static int InsertarProducto(Producto producto)
        {

            using (SqlConnection conection = new SqlConnection(cadenaDeConexion))
            {
                var query = "INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES(@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario)";

                SqlCommand comando = new SqlCommand(query, conection);

                comando.Parameters.AddWithValue("@Descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@Costo", producto.Costo);
                comando.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@Stock", producto.Stock);
                comando.Parameters.AddWithValue("@IdUsuario", producto.IdUsuario);

                conection.Open();

                return comando.ExecuteNonQuery();

            }
        }


    }
}
