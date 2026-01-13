CREATE VIEW vwExercicioDetalhado AS
SELECT
    e.ExercicioId,
    e.Nome,
    e.GrupoMuscular,
    e.Equipamento,

    COUNT(DISTINCT te.TreinoId) AS TotalTreinos,
    COUNT(te.TreinoExercicioId) AS TotalSeries,
    ISNULL(SUM(te.Repeticoes), 0) AS TotalRepeticoes
FROM Exercicio e
LEFT JOIN TreinoExercicio te ON te.ExercicioId = e.ExercicioId
GROUP BY
    e.ExercicioId,
    e.Nome,
    e.GrupoMuscular,
    e.Equipamento;
