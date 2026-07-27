-- =============================================================================
-- Modulo Exercicio - porte de T-SQL para PostgreSQL.
--
-- Nota sobre paginacao: a procedure original devolvia DOIS result sets (o total
-- e a pagina) e era lida com QueryMultipleAsync. Function no Postgres devolve um
-- unico conjunto, entao foi dividida em duas: "spExercicioContarTodos" e
-- "spExercicioPaginacao". O repositorio faz as duas chamadas.
--
-- O arquivo T-SQL Views/Exercicio/vwExercicioObterDetalhes.sql foi ignorado de
-- proposito: definia a mesma view que vwExercicioDetalhado, porem referenciando
-- as tabelas "Exercicio" e "TreinoExercicio" no singular, que nunca existiram.
-- =============================================================================


-- -----------------------------------------------------------------------------
-- Funcao escalar
-- -----------------------------------------------------------------------------

-- COUNT() no Postgres devolve BIGINT; o cast mantem o INTEGER que o C# espera.
CREATE OR REPLACE FUNCTION "fnExercicioTotalTreino"(p_exercicio_id INTEGER)
RETURNS INTEGER
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT COALESCE(COUNT(*), 0)::INTEGER
    FROM "TreinoExercicios"
    WHERE "ExercicioId" = p_exercicio_id;
$$;


-- -----------------------------------------------------------------------------
-- "Stored procedures"
-- -----------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION "spExercicioCriar"(
    p_nome           VARCHAR(150),
    p_grupo_muscular INTEGER,
    p_equipamento    VARCHAR(80),
    p_descricao      TEXT,
    p_imagem_url     TEXT,
    p_video_url      TEXT
)
RETURNS INTEGER
LANGUAGE sql
SET search_path = public
AS $$
    INSERT INTO "Exercicios" ("Nome", "GrupoMuscular", "Equipamento", "Descricao", "ImagemUrl", "VideoUrl")
    VALUES (p_nome, p_grupo_muscular, p_equipamento, p_descricao, p_imagem_url, p_video_url)
    RETURNING "ExercicioId";
$$;

CREATE OR REPLACE FUNCTION "spExercicioAtualizar"(
    p_exercicio_id   INTEGER,
    p_nome           VARCHAR(150),
    p_grupo_muscular INTEGER,
    p_equipamento    VARCHAR(80),
    p_descricao      TEXT,
    p_imagem_url     TEXT,
    p_video_url      TEXT
)
RETURNS void
LANGUAGE sql
SET search_path = public
AS $$
    UPDATE "Exercicios"
    SET "Nome"          = p_nome,
        "GrupoMuscular" = p_grupo_muscular,
        "Equipamento"   = p_equipamento,
        "Descricao"     = p_descricao,
        "ImagemUrl"     = p_imagem_url,
        "VideoUrl"      = p_video_url
    WHERE "ExercicioId" = p_exercicio_id;
$$;

CREATE OR REPLACE FUNCTION "spExercicioDeletar"(p_exercicio_id INTEGER)
RETURNS void
LANGUAGE sql
SET search_path = public
AS $$
    DELETE FROM "Exercicios"
    WHERE "ExercicioId" = p_exercicio_id;
$$;

-- Devolve tambem Descricao, ImagemUrl e VideoUrl: a tela de catalogo mostra a
-- orientacao de execucao junto do exercicio, e sem esses campos na listagem o
-- front precisaria de uma requisicao extra por item so para exibir o texto.
CREATE OR REPLACE FUNCTION "spExercicioListar"()
RETURNS TABLE (
    "ExercicioId"   INTEGER,
    "Nome"          VARCHAR(150),
    "GrupoMuscular" INTEGER,
    "Equipamento"   VARCHAR(80),
    "Descricao"     TEXT,
    "ImagemUrl"     TEXT,
    "VideoUrl"      TEXT
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT e."ExercicioId", e."Nome", e."GrupoMuscular", e."Equipamento",
           e."Descricao", e."ImagemUrl", e."VideoUrl"
    FROM "Exercicios" e
    ORDER BY e."GrupoMuscular", e."Nome";
$$;

CREATE OR REPLACE FUNCTION "spExercicioObter"(p_exercicio_id INTEGER)
RETURNS TABLE (
    "ExercicioId"   INTEGER,
    "Nome"          VARCHAR(150),
    "GrupoMuscular" INTEGER,
    "Equipamento"   VARCHAR(80),
    "Descricao"     TEXT,
    "ImagemUrl"     TEXT,
    "VideoUrl"      TEXT
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT e."ExercicioId", e."Nome", e."GrupoMuscular", e."Equipamento",
           e."Descricao", e."ImagemUrl", e."VideoUrl"
    FROM "Exercicios" e
    WHERE e."ExercicioId" = p_exercicio_id;
$$;

CREATE OR REPLACE FUNCTION "spExercicioPorGrupoMuscular"(p_grupo_muscular INTEGER)
RETURNS TABLE (
    "ExercicioId"   INTEGER,
    "Nome"          VARCHAR(150),
    "GrupoMuscular" INTEGER,
    "Equipamento"   VARCHAR(80),
    "Descricao"     TEXT,
    "VideoUrl"      TEXT
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT e."ExercicioId", e."Nome", e."GrupoMuscular", e."Equipamento",
           e."Descricao", e."VideoUrl"
    FROM "Exercicios" e
    WHERE e."GrupoMuscular" = p_grupo_muscular
    ORDER BY e."Nome";
$$;

-- Primeira metade da procedure original (o SELECT COUNT(*)).
CREATE OR REPLACE FUNCTION "spExercicioContarTodos"()
RETURNS INTEGER
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT COUNT(*)::INTEGER FROM "Exercicios";
$$;

-- Segunda metade: OFFSET/FETCH do T-SQL vira LIMIT/OFFSET.
CREATE OR REPLACE FUNCTION "spExercicioPaginacao"(
    p_pagina         INTEGER,
    p_tamanho_pagina INTEGER
)
RETURNS TABLE (
    "ExercicioId"   INTEGER,
    "Nome"          VARCHAR(150),
    "GrupoMuscular" INTEGER,
    "Equipamento"   VARCHAR(80),
    "Descricao"     TEXT,
    "VideoUrl"      TEXT
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT e."ExercicioId", e."Nome", e."GrupoMuscular", e."Equipamento",
           e."Descricao", e."VideoUrl"
    FROM "Exercicios" e
    ORDER BY e."ExercicioId"
    LIMIT p_tamanho_pagina
    OFFSET (p_pagina - 1) * p_tamanho_pagina;
$$;


-- -----------------------------------------------------------------------------
-- Views
-- -----------------------------------------------------------------------------

CREATE OR REPLACE VIEW "vwExercicioResumo"
WITH (security_invoker = true) AS
SELECT e."ExercicioId",
       e."Nome",
       e."GrupoMuscular",
       e."Equipamento",
       e."VideoUrl",
       "fnExercicioTotalTreino"(e."ExercicioId") AS "TotalTreinos"
FROM "Exercicios" e;

CREATE OR REPLACE VIEW "vwExercicioDetalhado"
WITH (security_invoker = true) AS
SELECT e."ExercicioId",
       e."Nome",
       e."GrupoMuscular",
       e."Equipamento",
       e."Descricao",
       e."VideoUrl",
       COUNT(DISTINCT te."TreinoId")::INTEGER            AS "TotalTreinos",
       COUNT(te."TreinoExercicioId")::INTEGER            AS "TotalSeries",
       COALESCE(SUM(te."Repeticoes"), 0)::INTEGER        AS "TotalRepeticoes"
FROM "Exercicios" e
LEFT JOIN "TreinoExercicios" te ON te."ExercicioId" = e."ExercicioId"
GROUP BY e."ExercicioId", e."Nome", e."GrupoMuscular", e."Equipamento",
         e."Descricao", e."VideoUrl";

CREATE OR REPLACE VIEW "vwExercicioContagemGrupoMusucular"
WITH (security_invoker = true) AS
SELECT e."GrupoMuscular",
       COUNT(e."ExercicioId")::INTEGER AS "TotalExercicios"
FROM "Exercicios" e
GROUP BY e."GrupoMuscular";
