CREATE PROCEDURE spEvolucaoCriar
	@UsuarioId INT,
	@Pesokg DECIMAL(5,2),
	@CinturaCm DECIMAL(5,2)= NULL,
	@BracoCm DECIMAL(5,2)= NULL,
	@CoxaCm DECIMAL(5,2)=NULL
	AS
	BEGIN
	INSERT INTO Evolucoes (UsuarioId, Pesokg, CinturaCm, BracoCm, CoxaCm,DataRegistro)
		VALUES (@UsuarioId, @Pesokg, @CinturaCm, @BracoCm, @CoxaCm,GETDATE())
		SELECT SCOPE_IDENTITY() AS EvolucaoId;
	END