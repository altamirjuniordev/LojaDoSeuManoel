using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmbaladorPedidosApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConexaoController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ConexaoController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Testar()
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return Ok("Conexão bem-sucedida!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao conectar: {ex.Message}");
            }
        }
    }
}
