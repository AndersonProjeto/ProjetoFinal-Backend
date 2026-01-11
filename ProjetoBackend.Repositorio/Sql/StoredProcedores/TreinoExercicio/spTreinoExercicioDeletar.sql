CREATE PROCEDURE spTreinoExercicioDeletar
	@TreinoExercicioId INT
	AS
	BEGIN
	DELETE FROM TreinoExercicios
		WHERE TreinoExercicioId = @TreinoExercicioId;
	END