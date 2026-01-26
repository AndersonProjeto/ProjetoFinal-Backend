CREATE FUNCTION fnEvolucaoConsultarIMC(@UsuarioId INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @PesoAtual DECIMAL(5,2);
    DECLARE @AlturaCm DECIMAL(5,2);
    DECLARE @IMC DECIMAL(5,2);

    SELECT TOP 1
        @PesoAtual = PesoKg
    FROM Evolucoes
    WHERE UsuarioId = @UsuarioId
    ORDER BY DataRegistro DESC;

    SELECT @AlturaCm = AlturaCm
    FROM Usuarios
    WHERE UsuarioId = @UsuarioId;

    IF @AlturaCm IS NULL OR @AlturaCm <= 0 OR @PesoAtual IS NULL
        RETURN NULL;

    SET @IMC = @PesoAtual / POWER(@AlturaCm / 100.0, 2);

    RETURN ROUND(@IMC, 2);
END
