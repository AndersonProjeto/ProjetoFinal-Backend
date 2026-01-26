CREATE VIEW vwEvolucaoResumo
AS
SELECT
    u.UsuarioId,

    dbo.fnEvolucaoPesoInicial(u.UsuarioId) AS PesoInicial,
    dbo.fnEvolucaoPesoAtual(u.UsuarioId) AS PesoAtual,
    dbo.fnEvolucaoDiferencaPeso(u.UsuarioId) AS DiferencaPeso,

    dbo.fnEvolucaoCinturaInicial(u.UsuarioId) AS CinturaInicial,
    dbo.fnEvolucaoCinturaAtual(u.UsuarioId) AS CinturaAtual,
    dbo.fnEvolucaoDiferencaCintura(u.UsuarioId) AS DiferencaCintura,

    dbo.fnEvolucaoBracoInicial(u.UsuarioId) AS BracoInicial,
    dbo.fnEvolucaoBracoAtual(u.UsuarioId) AS BracoAtual,
    dbo.fnEvolucaoDiferencaBraco(u.UsuarioId) AS DiferencaBraco,

    dbo.fnEvolucaoCoxaInicial(u.UsuarioId) AS CoxaInicial,
    dbo.fnEvolucaoCoxaAtual(u.UsuarioId) AS CoxaAtual,
    dbo.fnEvolucaoDiferencaCoxa(u.UsuarioId) AS DiferencaCoxa,

    dbo.fnEvolucaoConsultarIMC(u.UsuarioId) AS IMC,

    (
        SELECT MAX(DataRegistro)
        FROM Evolucoes e
        WHERE e.UsuarioId = u.UsuarioId
    ) AS DataUltimaEvolucao
FROM Usuarios u;
