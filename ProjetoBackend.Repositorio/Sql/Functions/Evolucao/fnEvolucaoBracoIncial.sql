CREATE FUNCTION fnEvolucaoBracoInicial (@UsuarioId INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @BracoInicial DECIMAL(5,2);

    SELECT TOP 1 
        @BracoInicial = BracoCm
    FROM Evolucoes
    WHERE UsuarioId = @UsuarioId
      AND BracoCm IS NOT NULL
    ORDER BY DataRegistro ASC;

    RETURN @BracoInicial;
END
