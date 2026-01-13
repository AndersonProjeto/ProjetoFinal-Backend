CREATE PROCEDURE spUsuarioObterPorEmail
    @Email NVARCHAR(255)
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
        DataCriacao
    FROM Usuarios
    WHERE Email = @Email;
END
GO
