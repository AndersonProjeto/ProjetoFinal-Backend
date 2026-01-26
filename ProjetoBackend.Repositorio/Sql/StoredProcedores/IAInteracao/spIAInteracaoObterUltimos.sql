CREATE PROCEDURE spIAInteracaoObterUltimos
    @UsuarioId INT,
    @Quantidade INT
AS
BEGIN
    SELECT TOP (@Quantidade)
        IAInteracaoId,
        Pergunta,
        Resposta,
        DataHora,
        UsuarioId
    FROM IAInteracoes
    WHERE UsuarioId = @UsuarioId
    ORDER BY IAInteracaoId DESC;
END
