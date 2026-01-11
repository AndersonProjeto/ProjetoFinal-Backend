CREATE VIEW vwEvolucaoHistorico
AS
SELECT
    EvolucaoId,
    UsuarioId,
    PesoKg,
    CinturaCm,
    BracoCm,
    CoxaCm,
    DataRegistro
FROM Evolucoes;
