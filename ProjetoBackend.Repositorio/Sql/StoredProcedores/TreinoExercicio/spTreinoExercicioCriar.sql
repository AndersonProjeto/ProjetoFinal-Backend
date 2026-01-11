CREATE PROCEDURE spTreinoExercicioCriar
	@TreinoId INT,
	@ExercicioId INT,
	@Series INT,
	@Repeticoes INT,
	@DescansoSegundos INT
	AS
	BEGIN
	INSERT INTO TreinoExercicios (TreinoId, ExercicioId, Series, Repeticoes,DescansoSegundos)
		VALUES (@TreinoId, @ExercicioId, @Series, @Repeticoes, @DescansoSegundos);
		SELECT SCOPE_IDENTITY() as TreinoExercicioId;
	END