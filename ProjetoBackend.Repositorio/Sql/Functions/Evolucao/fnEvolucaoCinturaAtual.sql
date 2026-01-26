CREATE FUNCTION fnEvolucaoCinturaAtual (@UsuarioId INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @CinturaAtual DECIMAL(5,2);

    SELECT TOP 1 
        @CinturaAtual = CinturaCm
    FROM Evolucoes
    WHERE UsuarioId = @UsuarioId
      AND CinturaCm IS NOT NULL
    ORDER BY DataRegistro DESC;

    RETURN @CinturaAtual;
END
