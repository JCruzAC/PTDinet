namespace CapaDatos.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        string _message {  get; set; }
        bool _isMessageError { get; set; }
        Task<bool> Create(T entity);
        Task<List<T>> List(DateTime? FInicio = null, DateTime? FFin = null, string? TipoMovimento = null, string? NumeroDocumento = null);
    }
}
