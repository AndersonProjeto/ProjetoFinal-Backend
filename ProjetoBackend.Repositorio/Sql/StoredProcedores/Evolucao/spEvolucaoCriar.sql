Create PROCEDURE spEvolucaoCriar
    @UsuarioId    INT,
    @PesoKg       DECIMAL(5,2),
    @CinturaCm    DECIMAL(5,2) = NULL,
    @BracoCm      DECIMAL(5,2) = NULL,
    @CoxaCm       DECIMAL(5,2) = NULL,
    @DataRegistro DATETIME     = NULL
AS
BEGIN
    INSERT INTO Evolucoes (UsuarioId, PesoKg, CinturaCm, BracoCm, CoxaCm, DataRegistro)
    VALUES (
        @UsuarioId,
        @PesoKg,
        @CinturaCm,
        @BracoCm,
        @CoxaCm,
        ISNULL(@DataRegistro, GETDATE())
    );

    SELECT SCOPE_IDENTITY() AS EvolucaoId;
END