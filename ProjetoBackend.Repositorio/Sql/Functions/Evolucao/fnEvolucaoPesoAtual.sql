CREATE FUNCTION fnEvolucaoPesoAtual (@UsuarioId INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @PesoAtual DECIMAL(5,2);

    SELECT TOP 1 
        @PesoAtual = PesoKg
    FROM Evolucoes
    WHERE UsuarioId = @UsuarioId
    ORDER BY DataRegistro DESC;

    RETURN @PesoAtual;
END
