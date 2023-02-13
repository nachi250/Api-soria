using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWebAPI.Models;

namespace SistemaGestionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
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

        [HttpPost]
        public void CrearProducto(Producto producto)
        {
            ManejadorProducto.InsertarProducto(producto);
        }
    }
}
