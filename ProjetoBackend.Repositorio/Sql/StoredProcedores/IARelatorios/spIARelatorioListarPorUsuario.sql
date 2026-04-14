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
    FROM IARelatorios
    WHERE UsuarioId = @UsuarioId
    ORDER BY DataGerado DESC;
END;
