CREATE FUNCTION fnEvolucaoCoxaAtual (@UsuarioId INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @CoxaAtual DECIMAL(5,2);

    SELECT TOP 1 
        @CoxaAtual = CoxaCm
    FROM Evolucoes
    WHERE UsuarioId = @UsuarioId
      AND CoxaCm IS NOT NULL
    ORDER BY DataRegistro DESC;

    RETURN @CoxaAtual;
END
