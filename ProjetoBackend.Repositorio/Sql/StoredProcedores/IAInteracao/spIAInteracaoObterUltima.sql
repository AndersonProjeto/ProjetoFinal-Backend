CREATE PROCEDURE spIAInteracaoObterUltima
	@UsuarioId INT
	AS
	BEGIN
	SELECT TOP 1
		IAInteracaoId,
		Pergunta,
		Resposta,
		DataHora
	FROM IAInteracoes
	WHERE UsuarioId = @UsuarioId
	ORDER BY DataHora DESC;
	END