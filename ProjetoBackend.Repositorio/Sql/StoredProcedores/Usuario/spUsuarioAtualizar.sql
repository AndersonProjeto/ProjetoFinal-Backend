CREATE PROCEDURE spUsuarioAtualizar
    @UsuarioId INT,
    @Nome VARCHAR(150),
    @Email VARCHAR(150),
    @DataNascimento DATETIME,
    @AlturaCm DECIMAL(5,2),
    @AvatarSeed NVARCHAR(50),
    @AvatarEstilo NVARCHAR(50)
AS
BEGIN
    UPDATE Usuarios
    SET Nome = @Nome,
        Email = @Email,
        DataNascimento = @DataNascimento,
        AlturaCm = @AlturaCm,
        AvatarSeed = @AvatarSeed,
        AvatarEstilo = @AvatarEstilo
    WHERE UsuarioId = @UsuarioId;
END