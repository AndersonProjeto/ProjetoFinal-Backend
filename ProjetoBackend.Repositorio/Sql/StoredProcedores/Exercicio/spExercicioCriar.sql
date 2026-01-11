CREATE PROCEDURE spExercicioCriar
	@Nome NVARCHAR(150),
	@GrupoMuscular NVARCHAR(80),
	@Equipamento NVARCHAR(80)
	AS
	BEGIN
	INSERT INTO Exercicios (Nome, GrupoMuscular, Equipamento)
		VALUES (@Nome, @GrupoMuscular, @Equipamento)
		SELECT SCOPE_IDENTITY() AS ExercicioId;
	END