CREATE PROCEDURE spTreinoExercicioAtualizar
	@TreinoExercicioId INT,
	@Series INT,
	@Repeticoes INT,
	@DescansoSegundos INT
	AS
	BEGIN
	UPDATE TreinoExercicios
		SET Series = @Series,
			Repeticoes = @Repeticoes,
			DescansoSegundos = @DescansoSegundos
		WHERE TreinoExercicioId = @TreinoExercicioId;
	END