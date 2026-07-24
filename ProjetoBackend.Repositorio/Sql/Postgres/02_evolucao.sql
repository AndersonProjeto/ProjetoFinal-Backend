-- =============================================================================
-- Modulo Evolucao - porte de T-SQL (SQL Server) para PL/pgSQL (PostgreSQL).
--
-- Notas de porte:
--   * Identificadores ficam aspeados porque o EF Core cria as tabelas em
--     PascalCase ("Evolucoes", "UsuarioId"); sem aspas o Postgres rebaixaria
--     tudo para minusculo e nao encontraria as colunas.
--   * SELECT TOP 1 ... ORDER BY  ->  SELECT ... ORDER BY ... LIMIT 1
--   * DECIMAL(5,2) -> NUMERIC(5,2) | INT -> INTEGER | DATETIME2 -> TIMESTAMP
--   * Procedures que devolviam result set viram FUNCTIONS RETURNS TABLE, porque
--     no Postgres CREATE PROCEDURE nao retorna linhas para o Dapper.
--   * SCOPE_IDENTITY() -> INSERT ... RETURNING
--   * ISNULL -> COALESCE | GETDATE() -> NOW()
--
-- Ordem de execucao: rodar DEPOIS de 01_usuario.sql (a view de resumo depende
-- da tabela "Usuarios") e depois das tabelas criadas pela migration do EF.
-- =============================================================================


-- -----------------------------------------------------------------------------
-- Funcoes escalares: valores atuais (registro mais recente)
-- -----------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION "fnEvolucaoPesoAtual"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "PesoKg"
    FROM "Evolucoes"
    WHERE "UsuarioId" = p_usuario_id
    ORDER BY "DataRegistro" DESC
    LIMIT 1;
$$;

CREATE OR REPLACE FUNCTION "fnEvolucaoCinturaAtual"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "CinturaCm"
    FROM "Evolucoes"
    WHERE "UsuarioId" = p_usuario_id
      AND "CinturaCm" IS NOT NULL
    ORDER BY "DataRegistro" DESC
    LIMIT 1;
$$;

CREATE OR REPLACE FUNCTION "fnEvolucaoBracoAtual"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "BracoCm"
    FROM "Evolucoes"
    WHERE "UsuarioId" = p_usuario_id
      AND "BracoCm" IS NOT NULL
    ORDER BY "DataRegistro" DESC
    LIMIT 1;
$$;

CREATE OR REPLACE FUNCTION "fnEvolucaoCoxaAtual"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "CoxaCm"
    FROM "Evolucoes"
    WHERE "UsuarioId" = p_usuario_id
      AND "CoxaCm" IS NOT NULL
    ORDER BY "DataRegistro" DESC
    LIMIT 1;
$$;


-- -----------------------------------------------------------------------------
-- Funcoes escalares: valores iniciais (registro mais antigo)
-- -----------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION "fnEvolucaoPesoInicial"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "PesoKg"
    FROM "Evolucoes"
    WHERE "UsuarioId" = p_usuario_id
    ORDER BY "DataRegistro" ASC
    LIMIT 1;
$$;

CREATE OR REPLACE FUNCTION "fnEvolucaoCinturaInicial"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "CinturaCm"
    FROM "Evolucoes"
    WHERE "UsuarioId" = p_usuario_id
      AND "CinturaCm" IS NOT NULL
    ORDER BY "DataRegistro" ASC
    LIMIT 1;
$$;

CREATE OR REPLACE FUNCTION "fnEvolucaoBracoInicial"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "BracoCm"
    FROM "Evolucoes"
    WHERE "UsuarioId" = p_usuario_id
      AND "BracoCm" IS NOT NULL
    ORDER BY "DataRegistro" ASC
    LIMIT 1;
$$;

CREATE OR REPLACE FUNCTION "fnEvolucaoCoxaInicial"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "CoxaCm"
    FROM "Evolucoes"
    WHERE "UsuarioId" = p_usuario_id
      AND "CoxaCm" IS NOT NULL
    ORDER BY "DataRegistro" ASC
    LIMIT 1;
$$;


-- -----------------------------------------------------------------------------
-- Funcoes escalares: diferencas (atual - inicial)
-- NULL - NULL continua NULL, mesmo comportamento do T-SQL original.
-- -----------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION "fnEvolucaoDiferencaPeso"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "fnEvolucaoPesoAtual"(p_usuario_id) - "fnEvolucaoPesoInicial"(p_usuario_id);
$$;

CREATE OR REPLACE FUNCTION "fnEvolucaoDiferencaCintura"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "fnEvolucaoCinturaAtual"(p_usuario_id) - "fnEvolucaoCinturaInicial"(p_usuario_id);
$$;

CREATE OR REPLACE FUNCTION "fnEvolucaoDiferencaBraco"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "fnEvolucaoBracoAtual"(p_usuario_id) - "fnEvolucaoBracoInicial"(p_usuario_id);
$$;

CREATE OR REPLACE FUNCTION "fnEvolucaoDiferencaCoxa"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT "fnEvolucaoCoxaAtual"(p_usuario_id) - "fnEvolucaoCoxaInicial"(p_usuario_id);
$$;


-- -----------------------------------------------------------------------------
-- IMC do usuario, calculado sobre o peso mais recente.
-- Usa plpgsql por causa da guarda de altura invalida.
-- -----------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION "fnEvolucaoConsultarIMC"(p_usuario_id INTEGER)
RETURNS NUMERIC(5,2)
LANGUAGE plpgsql
STABLE
SET search_path = public
AS $$
DECLARE
    v_peso_atual NUMERIC(5,2);
    v_altura_cm  NUMERIC(5,2);
BEGIN
    SELECT "PesoKg"
    INTO v_peso_atual
    FROM "Evolucoes"
    WHERE "UsuarioId" = p_usuario_id
    ORDER BY "DataRegistro" DESC
    LIMIT 1;

    SELECT "AlturaCm"
    INTO v_altura_cm
    FROM "Usuarios"
    WHERE "UsuarioId" = p_usuario_id;

    IF v_altura_cm IS NULL OR v_altura_cm <= 0 OR v_peso_atual IS NULL THEN
        RETURN NULL;
    END IF;

    RETURN ROUND(v_peso_atual / POWER(v_altura_cm / 100.0, 2), 2);
END;
$$;


-- -----------------------------------------------------------------------------
-- "Stored procedures" -> functions.
-- Os nomes sp* foram mantidos para preservar a rastreabilidade com o projeto
-- original; no Postgres sao FUNCTIONS, chamadas com SELECT.
-- -----------------------------------------------------------------------------

-- Insere e devolve o id gerado (equivale ao SCOPE_IDENTITY do T-SQL).
CREATE OR REPLACE FUNCTION "spEvolucaoCriar"(
    p_usuario_id    INTEGER,
    p_peso_kg       NUMERIC(5,2),
    p_cintura_cm    NUMERIC(5,2) DEFAULT NULL,
    p_braco_cm      NUMERIC(5,2) DEFAULT NULL,
    p_coxa_cm       NUMERIC(5,2) DEFAULT NULL,
    p_data_registro TIMESTAMP    DEFAULT NULL
)
RETURNS INTEGER
LANGUAGE sql
SET search_path = public
AS $$
    INSERT INTO "Evolucoes" ("UsuarioId", "PesoKg", "CinturaCm", "BracoCm", "CoxaCm", "DataRegistro")
    VALUES (
        p_usuario_id,
        p_peso_kg,
        p_cintura_cm,
        p_braco_cm,
        p_coxa_cm,
        COALESCE(p_data_registro, NOW())
    )
    RETURNING "EvolucaoId";
$$;

-- Nao devolve linhas: o Dapper chama com ExecuteAsync sobre um SELECT.
CREATE OR REPLACE FUNCTION "spEvolucaoAtualizar"(
    p_evolucao_id INTEGER,
    p_peso_kg     NUMERIC(5,2),
    p_cintura_cm  NUMERIC(5,2) DEFAULT NULL,
    p_braco_cm    NUMERIC(5,2) DEFAULT NULL,
    p_coxa_cm     NUMERIC(5,2) DEFAULT NULL
)
RETURNS void
LANGUAGE sql
SET search_path = public
AS $$
    UPDATE "Evolucoes"
    SET "PesoKg"    = p_peso_kg,
        "CinturaCm" = p_cintura_cm,
        "BracoCm"   = p_braco_cm,
        "CoxaCm"    = p_coxa_cm
    WHERE "EvolucaoId" = p_evolucao_id;
$$;

CREATE OR REPLACE FUNCTION "spEvolucaoObter"(p_evolucao_id INTEGER)
RETURNS TABLE (
    "EvolucaoId"   INTEGER,
    "UsuarioId"    INTEGER,
    "PesoKg"       NUMERIC(5,2),
    "CinturaCm"    NUMERIC(5,2),
    "BracoCm"      NUMERIC(5,2),
    "CoxaCm"       NUMERIC(5,2),
    "DataRegistro" TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT e."EvolucaoId",
           e."UsuarioId",
           e."PesoKg",
           e."CinturaCm",
           e."BracoCm",
           e."CoxaCm",
           e."DataRegistro"
    FROM "Evolucoes" e
    WHERE e."EvolucaoId" = p_evolucao_id;
$$;

CREATE OR REPLACE FUNCTION "spEvolucaoObterUltima"(p_usuario_id INTEGER)
RETURNS TABLE (
    "EvolucaoId"   INTEGER,
    "UsuarioId"    INTEGER,
    "PesoKg"       NUMERIC(5,2),
    "CinturaCm"    NUMERIC(5,2),
    "BracoCm"      NUMERIC(5,2),
    "CoxaCm"       NUMERIC(5,2),
    "DataRegistro" TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT e."EvolucaoId",
           e."UsuarioId",
           e."PesoKg",
           e."CinturaCm",
           e."BracoCm",
           e."CoxaCm",
           e."DataRegistro"
    FROM "Evolucoes" e
    WHERE e."UsuarioId" = p_usuario_id
    ORDER BY e."DataRegistro" DESC
    LIMIT 1;
$$;

CREATE OR REPLACE FUNCTION "spEvolucaoListarPorUsuario"(p_usuario_id INTEGER)
RETURNS TABLE (
    "EvolucaoId"   INTEGER,
    "UsuarioId"    INTEGER,
    "PesoKg"       NUMERIC(5,2),
    "CinturaCm"    NUMERIC(5,2),
    "BracoCm"      NUMERIC(5,2),
    "CoxaCm"       NUMERIC(5,2),
    "DataRegistro" TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT e."EvolucaoId",
           e."UsuarioId",
           e."PesoKg",
           e."CinturaCm",
           e."BracoCm",
           e."CoxaCm",
           e."DataRegistro"
    FROM "Evolucoes" e
    WHERE e."UsuarioId" = p_usuario_id
    ORDER BY e."DataRegistro" ASC;
$$;


-- -----------------------------------------------------------------------------
-- Views
--
-- security_invoker = true e obrigatorio aqui: sem isso a view roda com as
-- permissoes do dono (postgres) e ignora o RLS das tabelas base, virando um
-- desvio para quem consultar via PostgREST com a anon key.
-- -----------------------------------------------------------------------------

CREATE OR REPLACE VIEW "vwEvolucaoHistorico"
WITH (security_invoker = true) AS
SELECT e."EvolucaoId",
       e."UsuarioId",
       e."PesoKg",
       e."CinturaCm",
       e."BracoCm",
       e."CoxaCm",
       e."DataRegistro"
FROM "Evolucoes" e;

CREATE OR REPLACE VIEW "vwEvolucaoResumo"
WITH (security_invoker = true) AS
SELECT u."UsuarioId",

       "fnEvolucaoPesoInicial"(u."UsuarioId")     AS "PesoInicial",
       "fnEvolucaoPesoAtual"(u."UsuarioId")       AS "PesoAtual",
       "fnEvolucaoDiferencaPeso"(u."UsuarioId")   AS "DiferencaPeso",

       "fnEvolucaoCinturaInicial"(u."UsuarioId")   AS "CinturaInicial",
       "fnEvolucaoCinturaAtual"(u."UsuarioId")     AS "CinturaAtual",
       "fnEvolucaoDiferencaCintura"(u."UsuarioId") AS "DiferencaCintura",

       "fnEvolucaoBracoInicial"(u."UsuarioId")     AS "BracoInicial",
       "fnEvolucaoBracoAtual"(u."UsuarioId")       AS "BracoAtual",
       "fnEvolucaoDiferencaBraco"(u."UsuarioId")   AS "DiferencaBraco",

       "fnEvolucaoCoxaInicial"(u."UsuarioId")     AS "CoxaInicial",
       "fnEvolucaoCoxaAtual"(u."UsuarioId")       AS "CoxaAtual",
       "fnEvolucaoDiferencaCoxa"(u."UsuarioId")   AS "DiferencaCoxa",

       "fnEvolucaoConsultarIMC"(u."UsuarioId")    AS "IMC",

       (SELECT MAX(e."DataRegistro")
        FROM "Evolucoes" e
        WHERE e."UsuarioId" = u."UsuarioId")      AS "DataUltimaEvolucao"
FROM "Usuarios" u;
