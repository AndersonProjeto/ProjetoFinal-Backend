CREATE FUNCTION fnTreinoTotalSeries
(
    @TreinoId INT
)
RETURNS INT
AS
BEGIN
    DECLARE @TotalSeries INT;

    SELECT @TotalSeries = SUM(Series)
    FROM TreinoExercicios
    WHERE TreinoId = @TreinoId;

    RETURN ISNULL(@TotalSeries, 0);
END
