CREATE PROCEDURE spExercicioCriar
    @Nome NVARCHAR(150),
    @GrupoMuscular INT ,
    @Equipamento NVARCHAR(80),
    @Descricao NVARCHAR(MAX),
    @ImagemUrl NVARCHAR(500)
AS
BEGIN
    INSERT INTO Exercicios (Nome, GrupoMuscular, Equipamento, Descricao, ImagemUrl)
    VALUES (@Nome, @GrupoMuscular, @Equipamento, @Descricao, @ImagemUrl)

    SELECT SCOPE_IDENTITY() AS ExercicioId;
END
