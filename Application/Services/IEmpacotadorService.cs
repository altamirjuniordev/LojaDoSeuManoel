using EmbaladorPedidosApi.Application.DTOs;

namespace EmbaladorPedidosApi.Application.Services
{
    public interface IEmpacotadorService
    {
        List<EmpacotamentoResponseDTO>Empacotar(List<PedidoRequestDTO> pedidos);
    }
}
