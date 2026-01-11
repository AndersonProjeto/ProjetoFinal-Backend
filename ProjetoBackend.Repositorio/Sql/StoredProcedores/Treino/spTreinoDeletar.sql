CREATE PROCEDURE spTreinoDeletar
	@TreinoId INT
	AS
	BEGIN
	DELETE FROM Treinos
		WHERE TreinoId = @TreinoId;
	END