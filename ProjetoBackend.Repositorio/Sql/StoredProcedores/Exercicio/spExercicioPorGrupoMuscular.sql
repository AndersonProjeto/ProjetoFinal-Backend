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
        Descricao,
        VideoUrl
    FROM Exercicios
    WHERE GrupoMuscular = @GrupoMuscular
    ORDER BY Nome;
END