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
    FROM IARelatorios
    WHERE UsuarioId = @UsuarioId
    ORDER BY DataGerado DESC;
END;