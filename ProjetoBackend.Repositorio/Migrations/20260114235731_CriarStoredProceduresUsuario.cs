using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class CriarStoredProceduresUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE spUsuarioCriar
                    @Nome NVARCHAR(150),
                    @Email NVARCHAR(150),
                    @SenhaHash NVARCHAR(100),
                    @DataNascimento DATE,
                    @AlturaCm DECIMAL(5,2)
                AS
                BEGIN
                    INSERT INTO Usuarios (Nome, Email, SenhaHash, DataNascimento, AlturaCm)
                    VALUES (@Nome, @Email, @SenhaHash, @DataNascimento, @AlturaCm)
                    SELECT SCOPE_IDENTITY() AS UsuarioId;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE spUsuarioAtualizar
                    @UsuarioId INT,
                    @Nome VARCHAR(150),
                    @Email VARCHAR(150),
                    @DataNascimento DATETIME,
                    @AlturaCm DECIMAL(5,2)
                AS
                BEGIN
                    UPDATE Usuarios
                    SET Nome = @Nome,
                        Email = @Email,
                        DataNascimento = @DataNascimento,
                        AlturaCm = @AlturaCm
                    WHERE UsuarioId = @UsuarioId;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE spUsuarioDeletar
                    @UsuarioId INT
                AS
                BEGIN
                    DELETE FROM Usuarios
                    WHERE UsuarioId = @UsuarioId;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE spUsuarioObter
                    @UsuarioId INT
                AS
                BEGIN
                    SELECT UsuarioId, Nome, Email, DataNascimento, AlturaCm
                    FROM Usuarios
                    WHERE UsuarioId = @UsuarioId;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE spUsuarioObterPorEmail
                    @Email NVARCHAR(255)
                AS
                BEGIN
                    SELECT UsuarioId, Nome, Email, SenhaHash, DataNascimento, AlturaCm, DataCriacao
                    FROM Usuarios
                    WHERE Email = @Email;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE spUsuarioListarPagina
                    @Pagina INT,
                    @TamanhoPagina INT
                AS
                BEGIN
                    SELECT UsuarioId, Nome, Email, DataNascimento, AlturaCm, DataCriacao
                    FROM Usuarios
                    ORDER BY UsuarioId
                    OFFSET (@Pagina - 1) * @TamanhoPagina ROWS
                    FETCH NEXT @TamanhoPagina ROWS ONLY;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spUsuarioCriar;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spUsuarioAtualizar;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spUsuarioDeletar;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spUsuarioObter;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spUsuarioObterPorEmail;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spUsuarioListarPagina;");
        }
    }
}