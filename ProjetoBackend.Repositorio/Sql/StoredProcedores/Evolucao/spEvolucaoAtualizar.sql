CREATE PROCEDURE spEvolucaoAtualizar
    @EvolucaoId INT,
    @PesoKg DECIMAL(5,2),
    @CinturaCm DECIMAL(5,2) = NULL,
    @BracoCm DECIMAL(5,2) = NULL,
    @CoxaCm DECIMAL(5,2) = NULL
AS
BEGIN
    UPDATE Evolucoes
    SET
        PesoKg = @PesoKg,
        CinturaCm = @CinturaCm,
        BracoCm = @BracoCm,
        CoxaCm = @CoxaCm
    WHERE EvolucaoId = @EvolucaoId;
END