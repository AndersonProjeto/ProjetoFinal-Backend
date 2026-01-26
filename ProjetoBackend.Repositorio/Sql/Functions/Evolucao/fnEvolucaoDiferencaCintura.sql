CREATE FUNCTION fnEvolucaoDiferencaCintura
(
    @UsuarioId INT
)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @Diferenca DECIMAL(5,2);

    SELECT @Diferenca =
           dbo.fnEvolucaoCinturaAtual(@UsuarioId)
           - dbo.fnEvolucaoCinturaInicial(@UsuarioId);

    RETURN @Diferenca;
END
