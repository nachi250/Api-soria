using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWebAPI.Models;

namespace SistemaGestionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost("{idUsuario}")]
        public void InsertSale(List<Producto> productos, int idUsuario)
        {
            ManejadorVenta.CargarVenta(idUsuario, productos);
        }

        [HttpGet("{idUsuario}")]
        public List<Venta> GetVentas(long id)
        {
            return ManejadorVenta.ObtenerVentas(id);
        }

    }
}
