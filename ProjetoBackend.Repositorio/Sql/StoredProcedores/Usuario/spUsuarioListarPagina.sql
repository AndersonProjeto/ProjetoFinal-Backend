CREATE PROCEDURE spUsuarioListarPagina
    @Pagina INT,
    @TamanhoPagina INT
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
    ORDER BY UsuarioId
    OFFSET (@Pagina - 1) * @TamanhoPagina ROWS
    FETCH NEXT @TamanhoPagina ROWS ONLY;
END