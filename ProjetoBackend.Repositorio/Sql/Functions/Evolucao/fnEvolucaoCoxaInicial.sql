CREATE FUNCTION fnEvolucaoCoxaInicial (@UsuarioId INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @CoxaInicial DECIMAL(5,2);

    SELECT TOP 1 
        @CoxaInicial = CoxaCm
    FROM Evolucoes
    WHERE UsuarioId = @UsuarioId
      AND CoxaCm IS NOT NULL
    ORDER BY DataRegistro ASC;

    RETURN @CoxaInicial;
END
