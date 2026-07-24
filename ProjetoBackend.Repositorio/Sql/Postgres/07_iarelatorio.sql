-- =============================================================================
-- Modulo IARelatorio - porte de T-SQL para PostgreSQL.
--
-- Atencao ao nome da tabela: e "IARelatorio" no SINGULAR. O EF a descobre pela
-- navegacao Usuario.IARelatorios e usa o nome do tipo, sem pluralizar. O arquivo
-- IARelatorioConfiguracoes.cs mapeia para "IARelatorios" (plural), mas nunca e
-- aplicado no OnModelCreating — se alguem aplicar, estas funcoes quebram.
-- =============================================================================


CREATE OR REPLACE FUNCTION "spIARelatorioCriar"(
    p_usuario_id INTEGER,
    p_relatorio  TEXT
)
RETURNS INTEGER
LANGUAGE sql
SET search_path = public
AS $$
    INSERT INTO "IARelatorio" ("UsuarioId", "Relatorio", "DataGerado")
    VALUES (p_usuario_id, p_relatorio, (NOW() AT TIME ZONE 'utc'))
    RETURNING "IARelatorioId";
$$;

CREATE OR REPLACE FUNCTION "spIARelatorioObterUltimo"(p_usuario_id INTEGER)
RETURNS TABLE (
    "IARelatorioId" INTEGER,
    "UsuarioId"     INTEGER,
    "Relatorio"     TEXT,
    "DataGerado"    TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT r."IARelatorioId", r."UsuarioId", r."Relatorio", r."DataGerado"
    FROM "IARelatorio" r
    WHERE r."UsuarioId" = p_usuario_id
    ORDER BY r."DataGerado" DESC
    LIMIT 1;
$$;

CREATE OR REPLACE FUNCTION "spIARelatorioListarPorUsuario"(p_usuario_id INTEGER)
RETURNS TABLE (
    "IARelatorioId" INTEGER,
    "UsuarioId"     INTEGER,
    "Relatorio"     TEXT,
    "DataGerado"    TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT r."IARelatorioId", r."UsuarioId", r."Relatorio", r."DataGerado"
    FROM "IARelatorio" r
    WHERE r."UsuarioId" = p_usuario_id
    ORDER BY r."DataGerado" DESC;
$$;
