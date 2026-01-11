CREATE PROCEDURE spTreinoAtualizar
	@TreinoId INT,
	@NomeTreino NVARCHAR(100)
	AS
	BEGIN
	UPDATE Treinos
	SET NomeTreino = @NomeTreino
	WHERE TreinoId = @TreinoId;
	END