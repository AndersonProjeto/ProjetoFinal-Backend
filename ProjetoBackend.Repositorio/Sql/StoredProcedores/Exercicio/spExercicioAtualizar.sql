CREATE PROCEDURE spExercicioAtualizar
	@ExercicioId INT,
	@Nome NVARCHAR(150),
	@GrupoMuscular NVARCHAR(80),
	@Equipamento NVARCHAR(80)
	AS
	BEGIN
	UPDATE Exercicios
		SET Nome = @Nome,
			GrupoMuscular = @GrupoMuscular,
			Equipamento = @Equipamento
		WHERE ExercicioId = @ExercicioId;
	END