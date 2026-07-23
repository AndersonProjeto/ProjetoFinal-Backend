CREATE PROCEDURE spExercicioCriar
    @Nome NVARCHAR(150),
    @GrupoMuscular INT,
    @Equipamento NVARCHAR(80),
    @Descricao NVARCHAR(MAX),
    @ImagemUrl NVARCHAR(500),
    @VideoUrl NVARCHAR(500)
AS
BEGIN
    INSERT INTO Exercicios (Nome, GrupoMuscular, Equipamento, Descricao, ImagemUrl, VideoUrl)
    VALUES (@Nome, @GrupoMuscular, @Equipamento, @Descricao, @ImagemUrl, @VideoUrl)

    SELECT SCOPE_IDENTITY() AS ExercicioId;
END