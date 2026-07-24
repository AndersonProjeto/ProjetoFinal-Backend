-- =============================================================================
-- Modulo Treino - porte de T-SQL para PostgreSQL.
-- =============================================================================


-- -----------------------------------------------------------------------------
-- Funcoes escalares (COUNT devolve BIGINT no Postgres, dai o cast para INTEGER)
-- -----------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION "fnTreinoTotalExercicios"(p_treino_id INTEGER)
RETURNS INTEGER
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT COUNT(*)::INTEGER
    FROM "TreinoExercicios"
    WHERE "TreinoId" = p_treino_id;
$$;

CREATE OR REPLACE FUNCTION "fnTreinoTotalUsuario"(p_usuario_id INTEGER)
RETURNS INTEGER
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT COUNT(*)::INTEGER
    FROM "Treinos"
    WHERE "UsuarioId" = p_usuario_id;
$$;


-- -----------------------------------------------------------------------------
-- "Stored procedures"
-- -----------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION "spTreinoCriar"(
    p_usuario_id  INTEGER,
    p_nome_treino VARCHAR(150)
)
RETURNS INTEGER
LANGUAGE sql
SET search_path = public
AS $$
    INSERT INTO "Treinos" ("UsuarioId", "NomeTreino", "DataCriacao")
    VALUES (p_usuario_id, p_nome_treino, NOW())
    RETURNING "TreinoId";
$$;

CREATE OR REPLACE FUNCTION "spTreinoAtualizar"(
    p_treino_id   INTEGER,
    p_nome_treino VARCHAR(150)
)
RETURNS void
LANGUAGE sql
SET search_path = public
AS $$
    UPDATE "Treinos"
    SET "NomeTreino" = p_nome_treino
    WHERE "TreinoId" = p_treino_id;
$$;

CREATE OR REPLACE FUNCTION "spTreinoDeletar"(p_treino_id INTEGER)
RETURNS void
LANGUAGE sql
SET search_path = public
AS $$
    DELETE FROM "Treinos"
    WHERE "TreinoId" = p_treino_id;
$$;

CREATE OR REPLACE FUNCTION "spTreinoObterPorID"(p_treino_id INTEGER)
RETURNS TABLE (
    "TreinoId"    INTEGER,
    "UsuarioId"   INTEGER,
    "NomeTreino"  VARCHAR(150),
    "DataCriacao" TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT t."TreinoId", t."UsuarioId", t."NomeTreino", t."DataCriacao"
    FROM "Treinos" t
    WHERE t."TreinoId" = p_treino_id;
$$;

CREATE OR REPLACE FUNCTION "spTreinoListarPorUsuario"(p_usuario_id INTEGER)
RETURNS TABLE (
    "TreinoId"    INTEGER,
    "UsuarioId"   INTEGER,
    "NomeTreino"  VARCHAR(150),
    "DataCriacao" TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT t."TreinoId", t."UsuarioId", t."NomeTreino", t."DataCriacao"
    FROM "Treinos" t
    WHERE t."UsuarioId" = p_usuario_id
    ORDER BY t."DataCriacao" DESC;
$$;


-- -----------------------------------------------------------------------------
-- Views
-- -----------------------------------------------------------------------------

CREATE OR REPLACE VIEW "vwTreinosPorUsuario"
WITH (security_invoker = true) AS
SELECT t."TreinoId",
       t."UsuarioId",
       t."NomeTreino",
       t."DataCriacao"
FROM "Treinos" t;

CREATE OR REPLACE VIEW "vwTreinoResumo"
WITH (security_invoker = true) AS
SELECT t."TreinoId",
       t."UsuarioId",
       t."NomeTreino",
       t."DataCriacao",
       "fnTreinoTotalExercicios"(t."TreinoId") AS "TotalExercicios"
FROM "Treinos" t;
