CREATE FUNCTION fnTreinoTotalUsuario
(
    @UsuarioId INT
)
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(*)
        FROM Treinos
        WHERE UsuarioId = @UsuarioId
    );
END