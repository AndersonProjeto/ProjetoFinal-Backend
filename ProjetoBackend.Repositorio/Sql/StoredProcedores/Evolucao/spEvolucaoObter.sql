CREATE PROCEDURE spEvolucaoObter
	@EvolucaoId INT
	AS
	BEGIN
	SELECT EvolucaoId,
        UsuarioId,
        PesoKg,
        CinturaCm,
        BracoCm,
        CoxaCm,
        DataRegistro
		FROM Evolucoes
		WHERE EvolucaoId = @EvolucaoId;
	END