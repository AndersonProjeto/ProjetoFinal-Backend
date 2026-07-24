-- =============================================================================
-- Modulo TreinoExercicio - porte de T-SQL para PostgreSQL.
-- =============================================================================


-- -----------------------------------------------------------------------------
-- "Stored procedures"
-- -----------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION "spTreinoExercicioCriar"(
    p_treino_id         INTEGER,
    p_exercicio_id      INTEGER,
    p_series            INTEGER,
    p_repeticoes        INTEGER,
    p_descanso_segundos INTEGER
)
RETURNS INTEGER
LANGUAGE sql
SET search_path = public
AS $$
    INSERT INTO "TreinoExercicios" ("TreinoId", "ExercicioId", "Series", "Repeticoes", "DescansoSegundos")
    VALUES (p_treino_id, p_exercicio_id, p_series, p_repeticoes, p_descanso_segundos)
    RETURNING "TreinoExercicioId";
$$;

CREATE OR REPLACE FUNCTION "spTreinoExercicioAtualizar"(
    p_treino_exercicio_id INTEGER,
    p_series              INTEGER,
    p_repeticoes          INTEGER,
    p_descanso_segundos   INTEGER
)
RETURNS void
LANGUAGE sql
SET search_path = public
AS $$
    UPDATE "TreinoExercicios"
    SET "Series"           = p_series,
        "Repeticoes"       = p_repeticoes,
        "DescansoSegundos" = p_descanso_segundos
    WHERE "TreinoExercicioId" = p_treino_exercicio_id;
$$;

CREATE OR REPLACE FUNCTION "spTreinoExercicioDeletar"(p_treino_exercicio_id INTEGER)
RETURNS void
LANGUAGE sql
SET search_path = public
AS $$
    DELETE FROM "TreinoExercicios"
    WHERE "TreinoExercicioId" = p_treino_exercicio_id;
$$;

CREATE OR REPLACE FUNCTION "spTreinoExercicioObter"(p_treino_exercicio_id INTEGER)
RETURNS TABLE (
    "TreinoExercicioId" INTEGER,
    "TreinoId"          INTEGER,
    "ExercicioId"       INTEGER,
    "Series"            INTEGER,
    "Repeticoes"        INTEGER,
    "DescansoSegundos"  INTEGER
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT te."TreinoExercicioId", te."TreinoId", te."ExercicioId",
           te."Series", te."Repeticoes", te."DescansoSegundos"
    FROM "TreinoExercicios" te
    WHERE te."TreinoExercicioId" = p_treino_exercicio_id;
$$;

CREATE OR REPLACE FUNCTION "spTreinoExercicioListarPorTreino"(p_treino_id INTEGER)
RETURNS TABLE (
    "TreinoExercicioId" INTEGER,
    "TreinoId"          INTEGER,
    "ExercicioId"       INTEGER,
    "NomeExercicio"     VARCHAR(150),
    "GrupoMuscular"     INTEGER,
    "Series"            INTEGER,
    "Repeticoes"        INTEGER,
    "DescansoSegundos"  INTEGER
)
LANGUAGE sql
STABLE
SET search_path = public
AS $$
    SELECT te."TreinoExercicioId", te."TreinoId", te."ExercicioId",
           e."Nome", e."GrupoMuscular",
           te."Series", te."Repeticoes", te."DescansoSegundos"
    FROM "TreinoExercicios" te
    INNER JOIN "Exercicios" e ON e."ExercicioId" = te."ExercicioId"
    WHERE te."TreinoId" = p_treino_id
    ORDER BY te."TreinoExercicioId";
$$;


-- -----------------------------------------------------------------------------
-- View
-- -----------------------------------------------------------------------------

CREATE OR REPLACE VIEW "vwTreinoExerciciosDetalhe"
WITH (security_invoker = true) AS
SELECT te."TreinoExercicioId",
       te."TreinoId",
       t."NomeTreino",
       e."ExercicioId",
       e."Nome" AS "NomeExercicio",
       e."GrupoMuscular",
       e."Equipamento",
       e."Descricao",
       te."Series",
       te."Repeticoes",
       te."DescansoSegundos"
FROM "TreinoExercicios" te
INNER JOIN "Treinos" t    ON t."TreinoId"    = te."TreinoId"
INNER JOIN "Exercicios" e ON e."ExercicioId" = te."ExercicioId";
