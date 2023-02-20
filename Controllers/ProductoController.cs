using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWebAPI.Models;

namespace SistemaGestionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<Producto> GetProductos(long id)
        {
            return ManejadorProducto.ObtenerProductos(id);
        }

        [HttpPost]
        public void CrearProducto(Producto producto)
        {
            ManejadorProducto.InsertarProducto(producto);
        }

        [HttpPut]
        public void ModificarProducto(Producto producto)
        {
            ManejadorProducto.ActualizarProducto(producto);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ManejadorProducto.DeleteProducto(id);
        }



    }
}
