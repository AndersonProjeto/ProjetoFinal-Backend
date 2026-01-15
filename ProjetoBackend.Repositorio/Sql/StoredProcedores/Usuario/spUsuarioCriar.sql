CREATE PROCEDURE spUsuarioCriar
	@Nome NVARCHAR(150),
	@Email NVARCHAR(150),
	@SenhaHash NVARCHAR(100),
	@DataNascimento DATE,
	@AlturaCm DECIMAL(5,2)

AS
BEGIN
	INSERT INTO Usuarios (Nome, Email, SenhaHash, DataNascimento, AlturaCm)
	VALUES (@Nome, @email, @SenhaHash, @DataNascimento, @AlturaCm)
	SELECT SCOPE_IDENTITY() AS UsuarioId;
END