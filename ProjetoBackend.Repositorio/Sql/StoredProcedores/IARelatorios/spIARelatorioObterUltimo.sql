CREATE PROCEDURE spIARelatorioObterUltimo
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT TOP 1
        IARelatorioId,
        UsuarioId,
        Relatorio,
        DataGerado
    FROM IARelatorio
    WHERE UsuarioId = @UsuarioId
    ORDER BY DataGerado DESC;
END;