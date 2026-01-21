CREATE PROCEDURE spUsuarioAtualizarSenha
    @UsuarioId INT,
    @SenhaHash NVARCHAR(255)
AS
BEGIN
    UPDATE Usuarios
    SET SenhaHash = @SenhaHash
    WHERE UsuarioId = @UsuarioId
END
