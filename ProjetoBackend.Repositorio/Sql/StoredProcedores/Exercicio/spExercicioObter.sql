CREATE PROCEDURE spExercicioObter
	@ExercicioId INT
	AS
	BEGIN
	SELECT
		ExercicioId,
		Nome,
		GrupoMuscular,
		Equipamento
	FROM Exercicios
	WHERE ExercicioId = @ExercicioId;
	END