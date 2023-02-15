using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWebAPI.Models;

namespace SistemaGestionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("{usuario}/{contraseña}")]
        public Usuario Login(string usuario, string contraseña)
        {
            return ManejadorUsuario.ObtenerUsuarioLogin(usuario, contraseña);
        }

        [HttpGet("{usuario}")]
        public Usuario GetUsuario(string nombreUsuario) 
        {
            return ManejadorUsuario.ObtenerUsuarioUser(nombreUsuario);
        }

        [HttpPost]
        public void CrearUsuario(Usuario usuario)
        {
            ManejadorUsuario.InsertarUsuario(usuario);
        }

        [HttpPut]
        public void UpdateUsuario(Usuario usuario)
        {
            ManejadorUsuario.ModificarUsuario(usuario);
        }
    }
}
