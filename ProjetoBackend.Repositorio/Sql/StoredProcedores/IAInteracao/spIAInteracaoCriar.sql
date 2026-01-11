CREATE PROCEDURE spIAInteracaoCriar
	@UsuarioId INT,
	@Pergunta NVARCHAR(500),
	@Resposta NVARCHAR(MAX)
	AS
	BEGIN
	INSERT INTO IAInteracoes (UsuarioId, Pergunta, Resposta, DataHora)
		VALUES (@UsuarioId, @Pergunta, @Resposta, GETUTCDATE());
		SELECT SCOPE_IDENTITY() as IAInteracaoId;
	END