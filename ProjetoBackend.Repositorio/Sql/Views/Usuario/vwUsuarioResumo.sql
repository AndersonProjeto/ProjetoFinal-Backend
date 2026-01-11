CREATE VIEW vwUsuarioResumo AS
SELECT
    u.UsuarioId,
    u.Nome,
    u.Email,
    u.AlturaCm,
    ue.PesoKg,
    dbo.fnCalcularIdade(u.DataNascimento) AS Idade,
    dbo.fnCalcularIMC(ue.PesoKg, u.AlturaCm) AS IMC,
    u.DataCriacao
FROM Usuarios u
LEFT JOIN vwUsuarioUltimaEvolucao ue
    ON ue.UsuarioId = u.UsuarioId;
