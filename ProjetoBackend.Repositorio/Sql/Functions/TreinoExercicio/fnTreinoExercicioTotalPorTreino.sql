CREATE FUNCTION fnTreinoTotalUsuario
(
    @TreinoId INT
)
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(*)
        FROM TreinoExercicios
        WHERE TreinoId = @TreinoId
    );
END