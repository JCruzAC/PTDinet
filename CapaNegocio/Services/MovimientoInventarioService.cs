using CapaDatos.Repositories;
using CapaEntidades;

namespace CapaNegocio.Services
{
    public class MovimientoInventarioService : IMovimientoInventarioService
    {
        private readonly IGenericRepository<MovimientoInventario> _repo;
        public string _message { get; set; }
        public bool _isMessageError { get; set; }
        public MovimientoInventarioService(IGenericRepository<MovimientoInventario> repo)
        {
            _repo = repo;
        }
        public async Task<bool> CrearMovimientoInventario(MovimientoInventario model)
        {
            if (!ValidarIngresoDatos(model))
                return false;
            bool valor = await _repo.Create(model);
            _isMessageError = _repo._isMessageError;
            _message = _repo._message;
            return valor;
        }
        private bool ValidarIngresoDatos(MovimientoInventario model)
        {
            //TODO:IMPLEMENTAR VALIDACION
            /*si es error
            _isMessageError = "MENSAJE DE ERROR";
            _message = true;
            */
            return true;
        }
        public async Task<List<MovimientoInventario>> ListadoMovimientoInventario(DateTime? FInicio = null, DateTime? FFin = null, string? TipoMovimento = null, string? NumeroDocumento = null)
        {
            List<MovimientoInventario> lista = await _repo.List(FInicio, FFin, TipoMovimento, NumeroDocumento);
            _isMessageError = _repo._isMessageError;
            _message = _repo._message;
            if (lista.Count == 0 && !_isMessageError)
                _message = "No exiten resultados";
            return lista;
        }
    }
}
