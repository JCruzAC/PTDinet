using CapaEntidades;

namespace CapaNegocio.Services
{
    public interface IMovimientoInventarioService
    {
        string _message { get; set; }
        bool _isMessageError { get; set; }
        Task<bool> CrearMovimientoInventario(MovimientoInventario model);
        Task<List<MovimientoInventario>> ListadoMovimientoInventario(DateTime? FInicio = null, DateTime? FFin = null, string? TipoMovimento = null, string? NumeroDocumento = null);
    }
}
