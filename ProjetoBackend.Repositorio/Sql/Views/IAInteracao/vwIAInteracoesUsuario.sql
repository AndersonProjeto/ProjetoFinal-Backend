CREATE VIEW vwIAInteracoesUsuario AS
SELECT
    IAInteracaoId,
    UsuarioId,
    Pergunta,
    Resposta,
    DataHora
FROM IAInteracoes;
