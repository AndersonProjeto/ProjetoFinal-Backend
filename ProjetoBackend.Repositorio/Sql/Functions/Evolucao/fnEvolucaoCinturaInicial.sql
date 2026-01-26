CREATE FUNCTION fnEvolucaoCinturaInicial (@UsuarioId INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @CinturaInicial DECIMAL(5,2);

    SELECT TOP 1 
        @CinturaInicial = CinturaCm
    FROM Evolucoes
    WHERE UsuarioId = @UsuarioId
      AND CinturaCm IS NOT NULL
    ORDER BY DataRegistro ASC;

    RETURN @CinturaInicial;
END
