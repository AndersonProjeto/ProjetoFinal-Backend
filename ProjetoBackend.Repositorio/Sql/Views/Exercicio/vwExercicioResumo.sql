CREATE VIEW vwExercicioResumo AS
SELECT
	E.ExercicioId,
	E.Nome,
	E.GrupoMuscular,
	E.Equipamento,
	e.VideoUrl,
	dbo.fnExercicioTotalTreino(E.ExercicioId) AS TotalTreinos
	FROM Exercicios E;