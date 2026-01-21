CREATE PROCEDURE spExercicioPorGrupoMuscular
(
@GrupoMuscular INT
)
AS
BEGIN
	SELECT
		ExercicioId,
		Nome,
		GrupoMuscular,
		Equipamento,
		descricao
	FROM Exercicios
	WHERE GrupoMuscular = @GrupoMuscular
	ORDER BY Nome;
END