CREATE PROCEDURE spExercicioPorGrupoMuscular
(
@GrupoMuscular NVARCHAR(80)
)
AS
BEGIN
	SELECT
		ExercicioId,
		Nome,
		GrupoMuscular,
		Equipamento
	FROM Exercicios
	WHERE GrupoMuscular = @GrupoMuscular
	ORDER BY Nome;
END