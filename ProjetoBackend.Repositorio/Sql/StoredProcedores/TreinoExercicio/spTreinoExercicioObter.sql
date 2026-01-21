CREATE PROCEDURE spTreinoExercicioObter
    @TreinoExercicioId INT
AS
BEGIN
    SELECT
        TreinoExercicioId,
        TreinoId,
        ExercicioId,
        Series,
        Repeticoes,
        DescansoSegundos
    FROM TreinoExercicios
    WHERE TreinoExercicioId = @TreinoExercicioId;
END
