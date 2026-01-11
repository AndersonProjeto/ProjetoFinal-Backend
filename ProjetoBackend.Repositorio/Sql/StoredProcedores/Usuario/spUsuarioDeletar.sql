CREATE PROCEDURE spUsuarioDeletar
	@UsuarioId INT
	AS
	BEGIN
		DELETE FROM Usuarios
		WHERE UsuarioId = @UsuarioId;
	END