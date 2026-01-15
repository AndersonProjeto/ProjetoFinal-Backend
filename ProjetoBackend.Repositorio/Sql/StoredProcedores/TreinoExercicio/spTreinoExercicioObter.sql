CREATE PROCEDURE spTreinoExercicioObter
	@TreinoExercicioId INT
	AS
	BEGIN
	SELECT TreinoExercicioId,
        TreinoId,
        ExercicioId,
        Series,
        Repeticoes,
        DescansoSegundos
		FROM TreinosExercicios
		WHERE TreinoExercicioId = @TreinoExercicioId;
	END