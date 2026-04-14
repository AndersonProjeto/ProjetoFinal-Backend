Create PROCEDURE spExercicioObter
    @ExercicioId INT
AS
BEGIN
    SELECT
        ExercicioId,
        Nome,
        GrupoMuscular,
        Equipamento,
        Descricao,
        ImagemUrl,
        VideoUrl
    FROM Exercicios
    WHERE ExercicioId = @ExercicioId;
END