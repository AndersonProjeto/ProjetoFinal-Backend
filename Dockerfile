# Imagem da API para o Railway.
#
# A porta nao e fixada aqui: o Railway injeta PORT em tempo de execucao e o
# Program.cs faz o binding nela (ver builder.WebHost.UseUrls). O EXPOSE abaixo e
# so documentacao do valor padrao quando PORT nao vem definida.

# ── build ────────────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar so os .csproj antes do restante do codigo faz o restore virar uma camada
# de cache, reaproveitada enquanto as dependencias nao mudarem.
COPY ProjetoBackend.sln .
COPY ProjetoBackend.API/ProjetoBackend.API.csproj              ProjetoBackend.API/
COPY ProjetoBackend.Aplicacao/ProjetoBackend.Aplicacao.csproj  ProjetoBackend.Aplicacao/
COPY ProjetoBackend.Dominio/ProjetoBackend.Dominio.csproj      ProjetoBackend.Dominio/
COPY ProjetoBackend.Repositorio/ProjetoBackend.Repositorio.csproj ProjetoBackend.Repositorio/
COPY ProjetoBackend.Services/ProjetoBackend.Services.csproj    ProjetoBackend.Services/
RUN dotnet restore ProjetoBackend.sln

COPY . .
RUN dotnet publish ProjetoBackend.API/ProjetoBackend.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ── runtime ──────────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Usuario sem privilegios ja provisionado nas imagens .NET 8.
USER $APP_UID

EXPOSE 8080

ENTRYPOINT ["dotnet", "ProjetoBackend.API.dll"]
