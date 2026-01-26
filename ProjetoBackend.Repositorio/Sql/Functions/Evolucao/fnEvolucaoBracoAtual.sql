CREATE FUNCTION fnEvolucaoBracoAtual (@UsuarioId INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @BracoAtual DECIMAL(5,2);

    SELECT TOP 1 
        @BracoAtual = BracoCm
    FROM Evolucoes
    WHERE UsuarioId = @UsuarioId
      AND BracoCm IS NOT NULL
    ORDER BY DataRegistro DESC;

    RETURN @BracoAtual;
END
