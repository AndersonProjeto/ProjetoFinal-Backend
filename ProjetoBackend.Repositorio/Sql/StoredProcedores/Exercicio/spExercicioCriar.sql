CREATE PROCEDURE spExercicioCriar
	@Nome NVARCHAR(150),
	@GrupoMuscular NVARCHAR(80),
	@Equipamento NVARCHAR(80),
	@Descricao NVARCHAR(MAX)
	AS
	BEGIN
	INSERT INTO Exercicios (Nome, GrupoMuscular, Equipamento, Descricao)
		VALUES (@Nome, @GrupoMuscular, @Equipamento, @Descricao)
		SELECT SCOPE_IDENTITY() AS ExercicioId;
	END