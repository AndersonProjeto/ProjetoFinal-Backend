CREATE PROCEDURE spUsuarioObter
	@UsuarioId INT
	AS
	BEGIN
	SELECT 
    UsuarioId,
    Nome,
    Email,
    DataNascimento,
    AlturaCm,
    DataCriacao
FROM Usuarios
WHERE UsuarioId = @UsuarioId;
	END