CREATE FUNCTION fnExercicioTotalTreino
(
	@ExercicioId INT
)
RETURNS INT
AS
BEGIN
	DECLARE @TotalTreino INT;
	SELECT @TotalTreino = COUNT(*)
	FROM TreinoExercicios
	WHERE ExercicioId = @ExercicioId;
	RETURN ISNULL(@TotalTreino, 0);
END