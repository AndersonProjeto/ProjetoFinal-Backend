CREATE FUNCTION fnEvolucaoPesoInicial
(
    @UsuarioId INT
)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @PesoInicial DECIMAL(5,2);

    SELECT TOP 1 @PesoInicial = PesoKg
    FROM Evolucoes
    WHERE UsuarioId = @UsuarioId
    ORDER BY DataRegistro ASC;

    RETURN @PesoInicial;
END
