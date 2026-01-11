CREATE VIEW vwUsuarioUltimaEvolucao AS
SELECT
    u.UsuarioId,
    u.Nome,
    u.Email,
    e.PesoKg,
    e.DataRegistro
FROM Usuarios u
LEFT JOIN Evolucoes e
    ON e.UsuarioId = u.UsuarioId
    AND e.DataRegistro = (
        SELECT MAX(DataRegistro)
        FROM Evolucoes
        WHERE UsuarioId = u.UsuarioId
    );