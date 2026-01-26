CREATE FUNCTION fnEvolucaoDiferencaCoxa
(
    @UsuarioId INT
)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @Diferenca DECIMAL(5,2);

    SELECT @Diferenca =
           dbo.fnEvolucaoCoxaAtual(@UsuarioId)
           - dbo.fnEvolucaoCoxaInicial(@UsuarioId);

    RETURN @Diferenca;
END
