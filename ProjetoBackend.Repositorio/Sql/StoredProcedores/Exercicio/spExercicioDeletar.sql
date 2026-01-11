CREATE PROCEDURE spExercicioDeletar
	@ExercicioId INT
	AS
	BEGIN
	DELETE FROM Exercicios
		WHERE ExercicioId = @ExercicioId;
	END