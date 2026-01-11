CREATE PROCEDURE spTreinoObterPorID
	@TreinoId INT
	AS
	BEGIN
	SELECT TreinoId, UsuarioId, NomeTreino, DataCriacao
	FROM Treinos
		WHERE TreinoId = @TreinoId;
	END