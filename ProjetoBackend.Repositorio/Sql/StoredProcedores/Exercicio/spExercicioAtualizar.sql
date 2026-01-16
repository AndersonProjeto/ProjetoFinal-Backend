CREATE PROCEDURE spExercicioAtualizar
	@ExercicioId INT,
	@Nome NVARCHAR(150),
	@GrupoMuscular NVARCHAR(80),
	@Equipamento NVARCHAR(80),
	@Descricao NVARCHAR(MAX)
	AS
	BEGIN
	UPDATE Exercicios
		SET Nome = @Nome,
			GrupoMuscular = @GrupoMuscular,
			Equipamento = @Equipamento,
			Descricao = @Descricao
		WHERE ExercicioId = @ExercicioId;
	END