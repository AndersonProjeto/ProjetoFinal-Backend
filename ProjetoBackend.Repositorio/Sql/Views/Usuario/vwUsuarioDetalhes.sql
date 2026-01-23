CREATE VIEW vwUsuarioDetalhes AS
SELECT 
    u.UsuarioId,
    u.Nome,
    u.Email,
    u.DataNascimento,
    u.AlturaCm,
    AvatarEstilo,
    AvatarSeed,
    u.DataCriacao
FROM Usuarios u