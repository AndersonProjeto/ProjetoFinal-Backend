CREATE PROCEDURE spTreinoCriar
    @UsuarioId INT,
    @NomeTreino NVARCHAR(150)
AS
BEGIN
    INSERT INTO Treinos (UsuarioId, NomeTreino, DataCriacao)
    VALUES (@UsuarioId, @NomeTreino, GETDATE());

    SELECT SCOPE_IDENTITY() AS TreinoId;
END
