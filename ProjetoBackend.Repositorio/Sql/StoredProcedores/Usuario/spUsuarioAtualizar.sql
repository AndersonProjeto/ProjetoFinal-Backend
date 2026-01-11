CREATE PROCEDURE spUsuarioAtualizar
    @UsuarioId INT,
    @Nome VARCHAR(150),
    @Email VARCHAR(150),
    @DataNascimento DATETIME,
    @AlturaCm DECIMAL(5,2)
AS
BEGIN
    UPDATE Usuarios
    SET Nome = @Nome,
        Email = @Email,
        DataNascimento = @DataNascimento,
        AlturaCm = @AlturaCm
    WHERE UsuarioId = @UsuarioId;
END