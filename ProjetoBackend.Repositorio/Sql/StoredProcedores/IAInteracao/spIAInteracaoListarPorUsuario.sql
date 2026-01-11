CREATE PROCEDURE spIAInteracaoListarPorUsuario
	@UsuarioId INT
	AS
	BEGIN
	SELECT 
		IAInteracaoId,
		Pergunta,
		Resposta,
		DataHora
	FROM IAInteracoes
	WHERE UsuarioId = @UsuarioId
	ORDER BY DataHora DESC;
	END