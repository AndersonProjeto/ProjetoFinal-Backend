CREATE PROCEDURE spIARelatorioCriar
    @UsuarioId  INT,
    @Relatorio  NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
 
    INSERT INTO IARelatorio (UsuarioId, Relatorio, DataGerado)
    VALUES (@UsuarioId, @Relatorio, GETUTCDATE());
 
    SELECT SCOPE_IDENTITY() AS IARelatorioId;
END;
