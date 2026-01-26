CREATE FUNCTION fnEvolucaoDiferencaBraco
(
    @UsuarioId INT
)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @Diferenca DECIMAL(5,2);

    SELECT @Diferenca =
           dbo.fnEvolucaoBracoAtual(@UsuarioId)
           - dbo.fnEvolucaoBracoInicial(@UsuarioId);

    RETURN @Diferenca;
END
