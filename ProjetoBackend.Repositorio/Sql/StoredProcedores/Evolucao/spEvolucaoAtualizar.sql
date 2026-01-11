CREATE PROCEDURE spEvolucaoAtualizar
	@EvolucaoId INT
	As
	BEGIN
	Select
		EvolucaoId,
		UsuarioId,
		Pesokg,
		CinturaCm,
		BracoCm,
		CoxaCm,
		DataRegistro
	FROM Evolucoes
	WHERE UsuarioId = @UsuarioId	
	ORDER BY DataRegistro DESC;
	END