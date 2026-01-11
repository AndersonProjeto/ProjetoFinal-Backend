CREATE VIEW vwTreinoResumo AS
SELECT
    t.TreinoId,
    t.UsuarioId,
    t.NomeTreino,
    t.DataCriacao,
    dbo.fnTreinoTotalExercicios(t.TreinoId) AS TotalExercicios
FROM Treinos t;
