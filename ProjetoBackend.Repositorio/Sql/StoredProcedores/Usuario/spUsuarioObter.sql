CREATE PROCEDURE spUsuarioObter
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        UsuarioId,
        Nome,
        Email,
        SenhaHash,      
        DataNascimento,
        AlturaCm,
         AvatarSeed,
        AvatarEstilo,
        DataCriacao
    FROM Usuarios
    WHERE UsuarioId = @UsuarioId;
END

