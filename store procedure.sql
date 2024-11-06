CREATE PROCEDURE sp_ListaMovInventario
(
	@FechaInicio as DATE=null,
	@FechaFin as DATE=null,
	@TipoMovimiento as VARCHAR(50)=null,
	@NroDocumento as VARCHAR(50)=null
)
AS
BEGIN
	if (@FechaInicio <> NULL and @FechaFin <> null and @TipoMovimiento <> null and @NroDocumento <> null)
	BEGIN
		SELECT * --SELECCIONAR CAMPOS
		FROM dbo.MOV_INVENTARIO
	WHERE
		TIPO_MOVIMIENTO = @TipoMovimiento
		AND NRO_DOCUMENTO = @NroDocumento
		AND (FECHA_TRANSACCION BETWEEN @FechaFin AND @FechaFin)
	END
	ELSE
		SELECT * --SELECCIONAR CAMPOS
		FROM dbo.MOV_INVENTARIO
END

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
