USE DB_PRUEBATECNICADINET;
GO
drop PROCEDURE sp_ListaMovInventario;
GO
CREATE PROCEDURE sp_ListaMovInventario
(
	@FechaInicio as DATE=null,
	@FechaFin as DATE=null,
	@TipoMovimiento as VARCHAR(50)=null,
	@NroDocumento as VARCHAR(50)=null
)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    DECLARE @whereClause NVARCHAR(MAX) = '';

    IF @FechaInicio IS NOT NULL AND @FechaFin IS NOT NULL
        SET @whereClause = @whereClause + ' AND FECHA_TRANSACCION BETWEEN @FechaInicio AND @FechaFin';

    IF @TipoMovimiento IS NOT NULL
        SET @whereClause = @whereClause + ' AND TIPO_MOVIMIENTO = @TipoMovimiento';

    IF @NroDocumento IS NOT NULL
        SET @whereClause = @whereClause + ' AND NRO_DOCUMENTO = @NroDocumento';

    SET @sql = '
        SELECT *
        FROM dbo.MOV_INVENTARIO
        WHERE 1 = 1 ' + @whereClause;

    EXEC sp_executesql @sql,
                        N'@FechaInicio date, @FechaFin date, @TipoMovimiento varchar(50), @NroDocumento varchar(50)',
                        @FechaInicio, @FechaFin, @TipoMovimiento, @NroDocumento;
END
RETURN
CREATE PROCEDURE sp_InsertarMovInventario
(
	@CodCia as VARCHAR(50)
	,@CompaniaVenta3 as VARCHAR(50)
	,@AlmacenVenta as VARCHAR(50)
	,@TipoMovimiento as VARCHAR(50)
	,@TipoDocumento as VARCHAR(50)
	,@NroDocumento as VARCHAR(50)
	,@CodItem2 as VARCHAR(50)
	,@FechaTransaccion as DATETIME
	,@Proveedor as VARCHAR(50)
	,@AlmacenDestino as VARCHAR(50)
)
AS
BEGIN
	INSERT INTO dbo.MOV_INVENTARIO (COD_CIA,COMPANIA_VENTA_3,ALMACEN_VENTA,TIPO_MOVIMIENTO,TIPO_DOCUMENTO,NRO_DOCUMENTO,COD_ITEM_2,FECHA_TRANSACCION,PROVEEDOR,ALMACEN_DESTINO)
	VALUES(@CodCia,@CompaniaVenta3,@AlmacenVenta,@TipoMovimiento,@TipoDocumento,@NroDocumento,@CodItem2,CONVERT(VARCHAR(19),@FechaTransaccion,103),@Proveedor,@AlmacenDestino)
END
