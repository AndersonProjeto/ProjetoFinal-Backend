CREATE VIEW vwEvolucaoHistorico
AS
SELECT
    e.EvolucaoId,
    e.UsuarioId,
    e.PesoKg,
    e.CinturaCm,
    e.BracoCm,
    e.CoxaCm,
    e.DataRegistro
FROM Evolucoes e;
