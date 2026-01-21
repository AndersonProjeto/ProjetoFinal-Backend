CREATE PROCEDURE spIAInteracaoCriar
    @UsuarioId INT,
    @Pergunta NVARCHAR(500),
    @Resposta NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO IAInteracoes (UsuarioId, Pergunta, Resposta, DataHora)
    VALUES (@UsuarioId, @Pergunta, @Resposta, GETDATE());

    SELECT CAST(SCOPE_IDENTITY() AS INT);
END