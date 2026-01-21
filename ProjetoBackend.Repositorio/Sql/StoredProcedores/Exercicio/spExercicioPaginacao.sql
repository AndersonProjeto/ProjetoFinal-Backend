CREATE PROCEDURE spExercicioPaginacao
    @Pagina INT,
    @TamanhoPagina INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT = (@Pagina - 1) * @TamanhoPagina;

    SELECT COUNT(*) AS TotalItens
    FROM Exercicios;

    SELECT
        ExercicioId,
        Nome,
        GrupoMuscular,
        Equipamento,
        Descricao
    FROM Exercicios
    ORDER BY ExercicioId
    OFFSET @Offset ROWS
    FETCH NEXT @TamanhoPagina ROWS ONLY;
END
