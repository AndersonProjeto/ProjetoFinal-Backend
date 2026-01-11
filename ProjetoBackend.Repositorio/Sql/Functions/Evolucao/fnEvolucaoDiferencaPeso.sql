CREATE FUNCTION fnEvolucaoDiferencaPeso
(
    @UsuarioId INT
)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @Diferenca DECIMAL(5,2);

    SELECT @Diferenca =
           dbo.fnEvolucaoPesoAtual(@UsuarioId)
           - dbo.fnEvolucaoPesoInicial(@UsuarioId);

    RETURN @Diferenca;
END