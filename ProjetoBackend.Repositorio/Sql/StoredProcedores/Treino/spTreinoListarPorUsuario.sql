CREATE PROCEDURE spTreinoListarPorUsuario
    @UsuarioId INT
AS
BEGIN
    SELECT
        TreinoId,
        UsuarioId,
        NomeTreino,
        DataCriacao
    FROM Treinos
    WHERE UsuarioId = @UsuarioId
    ORDER BY DataCriacao DESC;
END
