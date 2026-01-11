CREATE FUNCTION fnCalcularIMC
(
	@PesoKg DECIMAL(5,2),
	@AlturaCm decimal(5,2)
)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @AlturaMetros DECIMAL(5,2);
    DECLARE @IMC DECIMAL(5,2);

    IF @AlturaCm <= 0
        RETURN NULL;

    SET @AlturaMetros = @AlturaCm / 100;
    SET @IMC = @PesoKg / (@AlturaMetros * @AlturaMetros);

    RETURN @IMC;
END