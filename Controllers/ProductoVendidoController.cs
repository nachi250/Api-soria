using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWebAPI.Models;

namespace SistemaGestionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<Producto> GetProductosVenidos(long idUsuario)
        {
            return ManejadorProductoVendido.ObtenerProductosVendidosId(idUsuario);
        }

    }
}
