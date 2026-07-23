CREATE PROCEDURE spIARelatorioListarPorUsuario
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        IARelatorioId,
        UsuarioId,
        Relatorio,
        DataGerado
    FROM IARelatorio
    WHERE UsuarioId = @UsuarioId
    ORDER BY DataGerado DESC;
END;
