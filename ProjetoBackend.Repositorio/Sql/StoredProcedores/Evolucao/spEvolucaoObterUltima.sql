CREATE PROCEDURE spEvolucaoObterUltima
	@UsuarioId INT
	AS
	BEGIN
	SELECT TOP 1
		EvolucaoId,
        UsuarioId,
        PesoKg,
        CinturaCm,
        BracoCm,
        CoxaCm,
        DataRegistro
    FROM Evolucoes
	WHERE UsuarioId = @UsuarioId
	ORDER BY DataRegistro DESC;
	END