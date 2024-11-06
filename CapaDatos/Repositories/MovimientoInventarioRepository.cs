using CapaEntidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CapaDatos.Repositories
{
    public class MovimientoInventarioRepository : IGenericRepository<MovimientoInventario>
    {
        private readonly string _cadenaSQL = string.Empty;
        public string _message { get; set; }
        public bool _isMessageError { get; set; }
        public MovimientoInventarioRepository(IConfiguration configuracion)
        {
            _cadenaSQL = configuracion.GetConnectionString("StringDbApp");
        }
        public async Task<List<MovimientoInventario>> List(DateTime? FInicio=null, DateTime? FFin = null, string? TipoMovimento = null, string? NumeroDocumento = null)
        {

            List<MovimientoInventario> lista = new();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new("sp_ListaMovInventario", conexion);
                    
                    cmd.Parameters.AddWithValue("FechaInicio", FInicio);
                    cmd.Parameters.AddWithValue("FechaFin", FFin);
                    cmd.Parameters.AddWithValue("TipoMovimiento", TipoMovimento);
                    cmd.Parameters.AddWithValue("NroDocumento", NumeroDocumento);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = await cmd.ExecuteReaderAsync())
                        {
                            while(await dr.ReadAsync())
                            {
                                lista.Add(new MovimientoInventario
                                {
                                    COD_CIA = dr["COD_CIA"].ToString(),
                                    COMPANIA_VENTA_3 = dr["COMPANIA_VENTA_3"].ToString(),
                                    ALMACEN_VENTA = dr["ALMACEN_VENTA"].ToString(),
                                    TIPO_MOVIMIENTO = dr["TIPO_MOVIMIENTO"].ToString(),
                                    TIPO_DOCUMENTO = dr["TIPO_DOCUMENTO"].ToString(),
                                    NRO_DOCUMENTO = dr["NRO_DOCUMENTO"].ToString(),
                                    COD_ITEM_2 = dr["COD_ITEM_2"].ToString(),
                                    FECHA_TRANSACCION = Convert.ToDateTime( dr["FECHA_TRANSACCION"].ToString()),
                                    PROVEEDOR = dr["PROVEEDOR"].ToString(),
                                    ALMACEN_DESTINO = dr["ALMACEN_DESTINO"].ToString()
                                });
                            }
                        }
                    _message = string.Empty;
                    _isMessageError = false;
                }
                catch (Exception ex)
                {
                    _message = "Mensaje de error: "+ex.Message;
                    _isMessageError = true;
                }
            }
            return lista;
        }
        public async Task<bool> Create(MovimientoInventario entity)
        {
            bool retorno = false;
            try
            {

                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new("sp_InsertarMovInventario", conexion);
                    cmd.Parameters.AddWithValue("CodCia", entity.COD_CIA);
                    cmd.Parameters.AddWithValue("@CompaniaVenta3", entity.COMPANIA_VENTA_3);
                    cmd.Parameters.AddWithValue("@AlmacenVenta", entity.ALMACEN_VENTA);
                    cmd.Parameters.AddWithValue("TipoMovimiento",entity.TIPO_MOVIMIENTO);
                    cmd.Parameters.AddWithValue("TipoDocumento",entity.TIPO_DOCUMENTO);
                    cmd.Parameters.AddWithValue("NroDocumento", entity.NRO_DOCUMENTO);
                    cmd.Parameters.AddWithValue("CodItem2", entity.COD_ITEM_2);
                    cmd.Parameters.AddWithValue("FechaTransaccion", entity.FECHA_TRANSACCION);
                    cmd.Parameters.AddWithValue("Proveedor", entity.PROVEEDOR);
                    cmd.Parameters.AddWithValue("Almacen_Destino", entity.ALMACEN_DESTINO);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    if (filasAfectadas > 0)
                        retorno = true;
                }
            }
            catch (Exception ex)
            {
                _message = "Mensaje de error: " + ex.Message;
                _isMessageError = true;
                retorno = false;
            }
            return retorno;
        }
    }
}
