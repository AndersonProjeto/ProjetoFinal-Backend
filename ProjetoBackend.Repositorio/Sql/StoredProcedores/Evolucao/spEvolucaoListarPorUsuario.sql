CREATE PROCEDURE spEvolucaoListarPorUsuario
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        EvolucaoId,
        UsuarioId,
        PesoKg,
        CinturaCm,
        BracoCm,
        CoxaCm,
        DataRegistro
    FROM Evolucoes
    WHERE UsuarioId = @UsuarioId
    ORDER BY DataRegistro ASC;
END;