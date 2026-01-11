CREATE FUNCTION fnCalcularIdade
(
	@DataNascimento DATE
)
RETURNS INT
AS
BEGIN
	DECLARE @Idade INT;
	SET @Idade = DATEDIFF(YEAR, @DataNascimento, GETDATE());
	IF (MONTH(@DataNascimento) > MONTH(GETDATE())) 
		OR (MONTH(@DataNascimento) = MONTH(GETDATE()) AND DAY(@DataNascimento) > DAY(GETDATE()))
	BEGIN
		SET @Idade = @Idade - 1;
	END
	RETURN @Idade;
END;