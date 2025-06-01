using EmbaladorPedidosApi.Application.DTOs;
using EmbaladorPedidosApi.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbaladorPedidosApi.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IEmpacotadorService _empacotadorService;

        public PedidosController(IEmpacotadorService empacotadorService)
        {
            _empacotadorService = empacotadorService;
        }

        [HttpPost("empacotar")]
        public IActionResult EmpacotarPedido([FromBody] List<PedidoRequestDTO> pedidos)
        {
            var resultado = _empacotadorService.Empacotar(pedidos);
            return Ok(resultado);
        }
    }
}
