using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWebAPI.Models
{
    internal static class ManejadorUsuario
    {
        static string cadenaDeConexion = "Data Source=IGNACIO-PC\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static Usuario ObtenerUsuario(long id)
        {
            Usuario usuario = new Usuario();
           
            using (SqlConnection conn = new SqlConnection(cadenaDeConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE @Id=id", conn);
                comando.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    Usuario usuario1 = new Usuario();
                    usuario.Id = reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Contrasena = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);

                }
            }
            
            return usuario;
        }

        public static Usuario ObtenerUsuarioLogin(string usuario, string contrasena)
        {
            Usuario usuarioLogin = new Usuario();
            
            using (SqlConnection conn = new SqlConnection(cadenaDeConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE @NombreUsuario = NombreUsuario and @Contraseña = Contraseña", conn);
                comando.Parameters.AddWithValue("@NombreUsuario", usuario);
                comando.Parameters.AddWithValue("@Contraseña", contrasena);

                conn.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    Usuario usuario1 = new Usuario();
                    usuario1.Id = reader.GetInt64(0);
                    usuario1.Nombre = reader.GetString(1);
                    usuario1.Apellido = reader.GetString(2);
                    usuario1.NombreUsuario = reader.GetString(3);
                    usuario1.Contrasena = reader.GetString(4);
                    usuario1.Mail = reader.GetString(5);

                    usuarioLogin = usuario1;
                }
            }

            return usuarioLogin;
        }

        public static int InsertarUsuario(Usuario usuario)
        {

            using(SqlConnection conection = new SqlConnection(cadenaDeConexion))
            {
                var query = @"INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES(@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail)";

                SqlCommand comando = new SqlCommand(query, conection);

                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@Contraseña", usuario.Contrasena);
                comando.Parameters.AddWithValue("@Mail", usuario.Mail);

                conection.Open();

                return comando.ExecuteNonQuery();

            }
        }

        public static Usuario ModificarUsuario(Usuario usuario)
        {

            using (SqlConnection conection = new SqlConnection(cadenaDeConexion))

            {
                SqlCommand comando = new SqlCommand();

                comando.Connection = conection;
                comando.Connection.Open();
                comando.CommandText = @"UPDATE Usuario
                                        SET [Nombre]= @Nombre,
                                            [Apellido]= @Apellido, 
                                            [NombreUsuario]= @NombreUsuario, 
                                            [Contraseña]=@Contraseña,
                                            [Mail]=@Mail
                                        WHERE [Id]=@Id";


                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@Contraseña", usuario.Contrasena);
                comando.Parameters.AddWithValue("@Mail", usuario.Mail);
                comando.Parameters.AddWithValue("@Id", usuario.Id);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }

            return usuario;
        }


    }
}
