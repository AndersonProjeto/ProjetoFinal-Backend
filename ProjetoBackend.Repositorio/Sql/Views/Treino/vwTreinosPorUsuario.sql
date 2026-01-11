CREATE VIEW vwTreinosPorUsuario AS
SELECT
    t.TreinoId,
    t.UsuarioId,
    t.NomeTreino,
    t.DataCriacao
FROM Treinos t;