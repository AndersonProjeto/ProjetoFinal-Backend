CREATE VIEW vwEvolucaoResumo
AS
SELECT
    u.UsuarioId,
    dbo.fnEvolucaoPesoInicial(u.UsuarioId) AS PesoInicial,
    dbo.fnEvolucaoPesoAtual(u.UsuarioId) AS PesoAtual,
    dbo.fnEvolucaoDiferencaPeso(u.UsuarioId) AS DiferencaPeso
FROM Usuarios u;