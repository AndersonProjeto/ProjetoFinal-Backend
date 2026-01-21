CREATE PROCEDURE spExercicioAtualizar
	@ExercicioId INT,
	@Nome NVARCHAR(150),
	@GrupoMuscular INT ,
	@Equipamento NVARCHAR(80),
	@Descricao NVARCHAR(MAX),
	@ImagemUrl NVARCHAR(MAX)
	AS
	BEGIN
	UPDATE Exercicios
		SET Nome = @Nome,
			GrupoMuscular = @GrupoMuscular,
			Equipamento = @Equipamento,
			Descricao = @Descricao,
			ImagemUrl = @ImagemUrl
		WHERE ExercicioId = @ExercicioId;
	END