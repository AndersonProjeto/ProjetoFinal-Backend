CREATE PROCEDURE spExercicioListar
AS
BEGIN
    SELECT
        ExercicioId,
        Nome,
        GrupoMuscular,
        Equipamento
    FROM Exercicios
    ORDER BY GrupoMuscular, Nome;
END
