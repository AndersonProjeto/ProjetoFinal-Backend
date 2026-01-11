CREATE PROCEDURE spTreinoExercicioListarPorTreino
    @TreinoId INT
AS
BEGIN
    SELECT
        te.TreinoExercicioId,
        te.TreinoId,
        te.ExercicioId,
        e.Nome AS NomeExercicio,
        e.GrupoMuscular,
        te.Series,
        te.Repeticoes,
        te.DescansoSegundos
    FROM TreinoExercicios te
    INNER JOIN Exercicios e ON e.ExercicioId = te.ExercicioId
    WHERE te.TreinoId = @TreinoId
    ORDER BY te.TreinoExercicioId;
END