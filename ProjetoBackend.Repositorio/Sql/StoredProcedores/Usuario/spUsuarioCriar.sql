CREATE PROCEDURE spUsuarioCriar
	@Nome NVARCHAR(150),
	@Email NVARCHAR(150),
	@SenhaHash NVARCHAR(100),
	@DataNascimento DATE,
	@AlturaCm DECIMAL(5,2),
	@AvatarEstilo NVARCHAR(50),
    @AvatarSeed NVARCHAR(50)

AS
BEGIN
	INSERT INTO Usuarios (Nome, Email, SenhaHash, DataNascimento, AlturaCm, AvatarEstilo,AvatarSeed)
	VALUES (@Nome, @Email, @SenhaHash, @DataNascimento, @AlturaCm,@AvatarEstilo,@AvatarSeed)
	SELECT SCOPE_IDENTITY() AS UsuarioId;
END