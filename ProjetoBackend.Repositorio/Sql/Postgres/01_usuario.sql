-- =============================================================================
-- Modulo Usuario - porte de T-SQL para PostgreSQL.
-- Convencoes iguais as de 02_evolucao.sql (identificadores aspeados, procedures
-- que devolvem linhas viram FUNCTIONS RETURNS TABLE).
--
-- SET search_path = public em toda funcao: impede que um schema plantado no
-- search_path de quem chama sequestre a resolucao dos nomes de tabela.
-- =============================================================================


-- -----------------------------------------------------------------------------
-- Funcoes escalares
-- -----------------------------------------------------------------------------

-- O T-SQL fazia DATEDIFF(YEAR) e depois subtraia 1 quando o aniversario ainda
-- nao tinha chegado. AGE() ja devolve o intervalo exato, entao extrair os anos
-- da o mesmo resultado sem o ajuste manual.
CREATE OR REPLACE FUNCTION "fnCalcularIdade"(p_data_nascimento DATE)
RETURNS INTEGER
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT EXTRACT(YEAR FROM AGE(CURRENT_DATE, p_data_nascimento))::INTEGER;
$$;

CREATE OR REPLACE FUNCTION "fnCalcularIMC"(
    p_peso_kg   NUMERIC(5,2),
    p_altura_cm NUMERIC(5,2)
)
RETURNS NUMERIC(5,2)
LANGUAGE sql
IMMUTABLE
SET search_path = public
AS $$
    -- ROUND explicito e obrigatorio: diferente do SQL Server, o PostgreSQL IGNORA
    -- o modificador de escala em RETURNS NUMERIC(5,2), entao sem isso a divisao
    -- voltaria com precisao cheia (27.7777... em vez de 27.78).
    SELECT CASE
        WHEN p_altura_cm IS NULL OR p_altura_cm <= 0 THEN NULL
        ELSE ROUND(p_peso_kg / POWER(p_altura_cm / 100.0, 2), 2)
    END;
$$;


-- -----------------------------------------------------------------------------
-- "Stored procedures"
-- -----------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION "spUsuarioCriar"(
    p_nome            VARCHAR(150),
    p_email           VARCHAR(150),
    p_senha_hash      TEXT,
    p_data_nascimento TIMESTAMP,
    p_altura_cm       NUMERIC(5,2),
    p_avatar_estilo   VARCHAR(50),
    p_avatar_seed     VARCHAR(50)
)
RETURNS INTEGER
LANGUAGE sql
SET search_path = public
AS $$
    INSERT INTO "Usuarios" ("Nome", "Email", "SenhaHash", "DataNascimento", "AlturaCm", "AvatarEstilo", "AvatarSeed")
    VALUES (p_nome, p_email, p_senha_hash, p_data_nascimento, p_altura_cm, p_avatar_estilo, p_avatar_seed)
    RETURNING "UsuarioId";
$$;

CREATE OR REPLACE FUNCTION "spUsuarioAtualizar"(
    p_usuario_id      INTEGER,
    p_nome            VARCHAR(150),
    p_email           VARCHAR(150),
    p_data_nascimento TIMESTAMP,
    p_altura_cm       NUMERIC(5,2),
    p_avatar_seed     VARCHAR(50),
    p_avatar_estilo   VARCHAR(50)
)
RETURNS void
LANGUAGE sql
SET search_path = public
AS $$
    UPDATE "Usuarios"
    SET "Nome"           = p_nome,
        "Email"          = p_email,
        "DataNascimento" = p_data_nascimento,
        "AlturaCm"       = p_altura_cm,
        "AvatarSeed"     = p_avatar_seed,
        "AvatarEstilo"   = p_avatar_estilo
    WHERE "UsuarioId" = p_usuario_id;
$$;

CREATE OR REPLACE FUNCTION "spUsuarioAtualizarSenha"(
    p_usuario_id INTEGER,
    p_senha_hash TEXT
)
RETURNS void
LANGUAGE sql
SET search_path = public
AS $$
    UPDATE "Usuarios"
    SET "SenhaHash" = p_senha_hash
    WHERE "UsuarioId" = p_usuario_id;
$$;

CREATE OR REPLACE FUNCTION "spUsuarioDeletar"(p_usuario_id INTEGER)
RETURNS void
LANGUAGE sql
SET search_path = public
AS $$
    DELETE FROM "Usuarios"
    WHERE "UsuarioId" = p_usuario_id;
$$;

CREATE OR REPLACE FUNCTION "spUsuarioObter"(p_usuario_id INTEGER)
RETURNS TABLE (
    "UsuarioId"      INTEGER,
    "Nome"           VARCHAR(150),
    "Email"          VARCHAR(150),
    "SenhaHash"      TEXT,
    "DataNascimento" TIMESTAMP,
    "AlturaCm"       NUMERIC(5,2),
    "AvatarSeed"     VARCHAR(50),
    "AvatarEstilo"   VARCHAR(50),
    "DataCriacao"    TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT u."UsuarioId", u."Nome", u."Email", u."SenhaHash", u."DataNascimento",
           u."AlturaCm", u."AvatarSeed", u."AvatarEstilo", u."DataCriacao"
    FROM "Usuarios" u
    WHERE u."UsuarioId" = p_usuario_id;
$$;

CREATE OR REPLACE FUNCTION "spUsuarioObterPorEmail"(p_email VARCHAR(150))
RETURNS TABLE (
    "UsuarioId"      INTEGER,
    "Nome"           VARCHAR(150),
    "Email"          VARCHAR(150),
    "SenhaHash"      TEXT,
    "DataNascimento" TIMESTAMP,
    "AlturaCm"       NUMERIC(5,2),
    "AvatarSeed"     VARCHAR(50),
    "AvatarEstilo"   VARCHAR(50),
    "DataCriacao"    TIMESTAMP
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT u."UsuarioId", u."Nome", u."Email", u."SenhaHash", u."DataNascimento",
           u."AlturaCm", u."AvatarSeed", u."AvatarEstilo", u."DataCriacao"
    FROM "Usuarios" u
    WHERE u."Email" = p_email;
$$;


-- -----------------------------------------------------------------------------
-- Views (security_invoker obrigatorio: sem isso furam o RLS das tabelas base)
-- -----------------------------------------------------------------------------

CREATE OR REPLACE VIEW "vwUsuarioDetalhes"
WITH (security_invoker = true) AS
SELECT u."UsuarioId",
       u."Nome",
       u."Email",
       u."DataNascimento",
       u."AlturaCm",
       u."AvatarEstilo",
       u."AvatarSeed",
       u."DataCriacao"
FROM "Usuarios" u;

CREATE OR REPLACE VIEW "vwUsuarioUltimaEvolucao"
WITH (security_invoker = true) AS
SELECT u."UsuarioId",
       u."Nome",
       u."Email",
       e."PesoKg",
       e."DataRegistro"
FROM "Usuarios" u
LEFT JOIN "Evolucoes" e
       ON e."UsuarioId" = u."UsuarioId"
      AND e."DataRegistro" = (
            SELECT MAX(e2."DataRegistro")
            FROM "Evolucoes" e2
            WHERE e2."UsuarioId" = u."UsuarioId"
          );

CREATE OR REPLACE VIEW "vwUsuarioResumo"
WITH (security_invoker = true) AS
SELECT u."UsuarioId",
       u."Nome",
       u."Email",
       u."AlturaCm",
       ue."PesoKg",
       "fnCalcularIdade"(u."DataNascimento"::DATE)      AS "Idade",
       "fnCalcularIMC"(ue."PesoKg", u."AlturaCm")       AS "IMC",
       u."DataCriacao"
FROM "Usuarios" u
LEFT JOIN "vwUsuarioUltimaEvolucao" ue
       ON ue."UsuarioId" = u."UsuarioId";
