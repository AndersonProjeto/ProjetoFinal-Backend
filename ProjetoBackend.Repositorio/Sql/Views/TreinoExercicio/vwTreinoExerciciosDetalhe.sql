CREATE VIEW vwTreinoExerciciosDetalhe 
AS
SELECT
    te.TreinoExercicioId,
    te.TreinoId,
    t.NomeTreino,
    e.ExercicioId,
    e.Nome AS NomeExercicio,
    e.GrupoMuscular,
    e.Equipamento,
    te.Series,
    te.Repeticoes,
    te.DescansoSegundos
FROM TreinoExercicios te
INNER JOIN Treinos t ON t.TreinoId = te.TreinoId
INNER JOIN Exercicios e ON e.ExercicioId = te.ExercicioId;