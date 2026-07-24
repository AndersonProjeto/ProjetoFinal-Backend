-- =============================================================================
-- Modulo IAInteracao - porte de T-SQL para PostgreSQL.
-- =============================================================================


-- -----------------------------------------------------------------------------
-- "Stored procedures"
-- -----------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION "spIAInteracaoCriar"(
    p_usuario_id INTEGER,
    p_pergunta   TEXT,
    p_resposta   TEXT
)
RETURNS INTEGER
LANGUAGE sql
SET search_path = public
AS $$
    INSERT INTO "IAInteracoes" ("UsuarioId", "Pergunta", "Resposta", "DataHora")
    VALUES (p_usuario_id, p_pergunta, p_resposta, NOW())
    RETURNING "IAInteracaoId";
$$;

CREATE OR REPLACE FUNCTION "spIAInteracaoListarPorUsuario"(p_usuario_id INTEGER)
RETURNS TABLE (
    "IAInteracaoId" INTEGER,
    "Pergunta"      TEXT,
    "Resposta"      TEXT,
    "DataHora"      TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT i."IAInteracaoId", i."Pergunta", i."Resposta", i."DataHora"
    FROM "IAInteracoes" i
    WHERE i."UsuarioId" = p_usuario_id
    ORDER BY i."DataHora" DESC;
$$;

CREATE OR REPLACE FUNCTION "spIAInteracaoObterUltima"(p_usuario_id INTEGER)
RETURNS TABLE (
    "IAInteracaoId" INTEGER,
    "Pergunta"      TEXT,
    "Resposta"      TEXT,
    "DataHora"      TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT i."IAInteracaoId", i."Pergunta", i."Resposta", i."DataHora"
    FROM "IAInteracoes" i
    WHERE i."UsuarioId" = p_usuario_id
    ORDER BY i."DataHora" DESC
    LIMIT 1;
$$;

-- SELECT TOP (@Quantidade) vira LIMIT parametrizado.
CREATE OR REPLACE FUNCTION "spIAInteracaoObterUltimos"(
    p_usuario_id INTEGER,
    p_quantidade INTEGER
)
RETURNS TABLE (
    "IAInteracaoId" INTEGER,
    "Pergunta"      TEXT,
    "Resposta"      TEXT,
    "DataHora"      TIMESTAMP,
    "UsuarioId"     INTEGER
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT i."IAInteracaoId", i."Pergunta", i."Resposta", i."DataHora", i."UsuarioId"
    FROM "IAInteracoes" i
    WHERE i."UsuarioId" = p_usuario_id
    ORDER BY i."IAInteracaoId" DESC
    LIMIT p_quantidade;
$$;


-- -----------------------------------------------------------------------------
-- View
-- -----------------------------------------------------------------------------

CREATE OR REPLACE VIEW "vwIAInteracoesUsuario"
WITH (security_invoker = true) AS
SELECT i."IAInteracaoId",
       i."UsuarioId",
       i."Pergunta",
       i."Resposta",
       i."DataHora"
FROM "IAInteracoes" i;
