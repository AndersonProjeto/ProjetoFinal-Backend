CREATE VIEW vwExercicioContagemGrupoMusucular AS
SELECT
	GrupoMuscular,
	COUNT(ExercicioId) AS TotalExercicios
	FROM Exercicios
	GROUP BY GrupoMuscular;
	