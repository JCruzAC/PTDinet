namespace CapaEntidades
{
    public class MovimientoInventario
    {
        public string COD_CIA { get; set; } = null!;
        public string COMPANIA_VENTA_3 { get; set; } = null!;
        public string ALMACEN_VENTA { get; set; } = null!;
        public string TIPO_MOVIMIENTO { get; set; } = null!;
        public string TIPO_DOCUMENTO { get; set; } = null!;
        public string NRO_DOCUMENTO { get; set; } = null!;
        public string COD_ITEM_2 { get; set; } = null!;
        public DateTime FECHA_TRANSACCION { get; set; }
        public string PROVEEDOR { get; set; } = null!;
        public string ALMACEN_DESTINO { get; set; } = null!;
    }
}
