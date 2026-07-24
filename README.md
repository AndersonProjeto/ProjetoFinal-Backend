# ProjetoFinal-Backend

O ACADIA é um sistema fitness inteligente que permite o gerenciamento de usuários, treinos, exercícios, evolução corporal e interações com Inteligência Artificial.
O backend é responsável por toda a lógica de negócio, persistência de dados e exposição de uma API REST, que é consumida pelo frontend da aplicação.

O backend é responsável por:

- Regras de negócio

Persistência de dados

Comunicação com o banco de dados

Exposição de uma API REST

Integração com uma API externa de IA

## 🚀 Tecnologias Utilizadas

### Linguagens
- C#
- SQL

### Frameworks e Bibliotecas
- .NET 8 (ASP.NET Core)
- Dapper
- JWT
- BCrypt

## 📋 Requisitos
- .NET SDK 8.0
- PostgreSQL 15+ (o projeto usa uma instância no Supabase)

> **Máquina só com runtime .NET 10?** Os comandos `dotnet` precisam de roll-forward,
> porque a solution é `net8.0`:
> ```powershell
> $env:DOTNET_ROLL_FORWARD = "Major"
> dotnet build
> ```

## ⚙️ Configuração

Nenhum segredo fica versionado. O `appsettings.json` guarda apenas configuração
pública (endpoints, CORS, logging); o resto vem de **user-secrets** em
desenvolvimento e de **variáveis de ambiente** na nuvem.

| Chave | Obrigatória | O que é |
| --- | :---: | --- |
| `ConnectionStrings:DefaultConnection` | ✅ | String de conexão Npgsql do Supabase (Session pooler, porta 5432) |
| `Jwt:Key` | ✅ | Chave de assinatura HMAC-SHA256 (mínimo 32 caracteres) |
| `Jwt:Issuer` | ✅ | Emissor do token |
| `Jwt:Audience` | ✅ | Audiência do token |
| `GitHubModels:Token` | ✅ | Token do GitHub Models (chat da AcadIA) |
| `GitHubModels:ApiUrl` | — | Já no `appsettings.json` |
| `Groq:Token` | ✅ | Token da Groq (geração de relatórios) |
| `Groq:ApiUrl` | — | Já no `appsettings.json` |
| `ExerciseDb:ApiKey` | ✅ | Chave RapidAPI da ExerciseDB (importação de imagens) |
| `Cors:Origens` | — | Origens liberadas. Padrão: `http://localhost:5173` |
| `Admin:Emails` | — | E-mails com permissão de escrita no catálogo de exercícios. Vazio = catálogo somente leitura |

> Sem `GitHubModels:Token` / `Groq:Token` / `ExerciseDb:ApiKey` a API sobe normalmente,
> mas as rotas de IA e a importação de imagens respondem **500**.

### Desenvolvimento — user-secrets

```bash
cd ProjetoBackend.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=...;Port=5432;Database=postgres;Username=...;Password=...;SSL Mode=Require"
dotnet user-secrets set "Jwt:Key" "<chave-de-no-minimo-32-caracteres>"
dotnet user-secrets set "Jwt:Issuer" "ProjetoBackend"
dotnet user-secrets set "Jwt:Audience" "ProjetoBackendUsuario"
dotnet user-secrets set "GitHubModels:Token" "<token>"
dotnet user-secrets set "Groq:Token" "<token>"
dotnet user-secrets set "ExerciseDb:ApiKey" "<chave-rapidapi>"
```

### Produção — variáveis de ambiente

O separador `:` vira `__` (dois underscores), e **listas usam índice numérico**:

```
ConnectionStrings__DefaultConnection=Host=...;Port=5432;...
Jwt__Key=...
Jwt__Issuer=ProjetoBackend
Jwt__Audience=ProjetoBackendUsuario
GitHubModels__Token=...
Groq__Token=...
ExerciseDb__ApiKey=...
Cors__Origens__0=https://<seu-frontend>.vercel.app
Admin__Emails__0=voce@exemplo.com
```

⚠️ Errar `Cors__Origens__0` não gera erro visível no servidor: a API cai no padrão
`localhost:5173` e **todo** request do frontend falha por CORS no navegador.

## 🗄️ Banco de Dados
- PostgreSQL (Supabase)
- Functions (`RETURNS TABLE`, chamadas via Dapper)
- Views (com `security_invoker`)
- RLS habilitada em todas as tabelas

Os scripts ficam em `ProjetoBackend.Repositorio/Sql/Postgres/`, embarcados no
assembly como `EmbeddedResource` e aplicados pelas migrations do EF — não há passo
manual de SQL no Supabase. As migrations pendentes são aplicadas **na subida da
aplicação**.

Modelo de dados

<img width="1006" height="774" alt="image" src="https://github.com/user-attachments/assets/7d1de82c-9f93-47a6-ba14-3a38c3d81d49" />

Usuários
| Coluna         | Tipo         | Observações                 |
| -------------- | ------------ | --------------------------- |
| UsuarioId      | int          | PK (chave primária)         |
| Nome           | string       | Nome completo do usuário    |
| Email          | string       | Único                       |
| SenhaHash      | string       | Senha criptografada         |
| DataNascimento | datetime     | Data de nascimento          |
| AlturaCm       | decimal(5,2) | Altura em centímetros       |
| AvatarSeed     | string       | Seed do avatar              |
| AvatarEstilo   | string       | Estilo do avatar            |
| DataCriacao    | datetime     | Data de criação do registro |

Treinos
| Coluna      | Tipo     | Observações               |
| ----------- | -------- | ------------------------- |
| TreinoId    | int      | PK (chave primária)       |
| UsuarioId   | int      | FK → Usuarios             |
| NomeTreino  | string   | Nome do treino            |
| DataCriacao | datetime | Data de criação do treino |

Exercícios
| Coluna        | Tipo   | Observações               |
| ------------- | ------ | ------------------------- |
| ExercicioId   | int    | PK (chave primária)       |
| Nome          | string | Nome do exercício         |
| GrupoMuscular | string | Grupo muscular trabalhado |
| Equipamento   | string | Equipamento utilizado     |
| Descricao     | string | Descrição do exercício    |
| ImagemUrl     | string | URL da imagem ilustrativa |

TreinoExercicios
| Coluna            | Tipo | Observações                   |
| ----------------- | ---- | ----------------------------- |
| TreinoExercicioId | int  | PK (chave primária)           |
| TreinoId          | int  | FK → Treinos                  |
| ExercicioId       | int  | FK → Exercicios               |
| Series            | int  | Quantidade de séries          |
| Repeticoes        | int  | Quantidade de repetições      |
| DescansoSegundos  | int  | Tempo de descanso em segundos |

Evoluções
| Coluna       | Tipo         | Observações                  |
| ------------ | ------------ | ---------------------------- |
| EvolucaoId   | int          | PK (chave primária)          |
| UsuarioId    | int          | FK → Usuarios                |
| PesoKg       | decimal(5,2) | Peso corporal                |
| CinturaCm    | decimal(5,2) | Medida da cintura            |
| BracoCm      | decimal(5,2) | Medida do braço              |
| CoxaCm       | decimal(5,2) | Medida da coxa               |
| DataRegistro | datetime     | Data do registro da evolução |

IAInteracoes
| Coluna        | Tipo     | Observações              |
| ------------- | -------- | ------------------------ |
| IAInteracaoId | int      | PK (chave primária)      |
| UsuarioId     | int      | FK → Usuarios            |
| Pergunta      | string   | Pergunta feita à IA      |
| Resposta      | string   | Resposta da IA           |
| DataHora      | datetime | Data e hora da interação |

##  Stored Procedures

As Stored Procedures são rotinas SQL armazenadas no banco de dados que encapsulam operações e regras de negócio, permitindo que a aplicação execute comandos de forma segura, organizada e eficiente.

No projeto ACADIA, as Stored Procedures são utilizadas principalmente para:
- Operações de CRUD (criar, atualizar, obter e excluir)
- Consultas filtradas e otimizações de leitura
- Geração de resumos e dados para dashboards
- Cálculos relacionados à evolução física do usuário
- Melhorar a experiência do usuário em consultas específicas

Elas são acessadas pela camada de repositório utilizando Dapper, garantindo alto desempenho, controle das consultas SQL e melhor organização da arquitetura do sistema.


Usuario

<img width="171" height="152" alt="image" src="https://github.com/user-attachments/assets/66987a75-f861-490b-9b04-8417cec2ee7f" />

Exercicio

<img width="191" height="153" alt="image" src="https://github.com/user-attachments/assets/aa720530-8543-4bf0-9dc1-924ab3c76b46" />

Treino

<img width="206" height="207" alt="image" src="https://github.com/user-attachments/assets/0c09dde7-fd13-4ae0-a7f2-4a64154731cb" />

TreinoExercicio

<img width="201" height="114" alt="image" src="https://github.com/user-attachments/assets/018fd8c7-02b1-4fe4-b260-3b8de8b7fa3c" />


IAintereacoes

<img width="192" height="91" alt="image" src="https://github.com/user-attachments/assets/06ddc706-6f1d-4b30-8f36-f5e873c35abb" />

Evolucoes

<img width="158" height="93" alt="image" src="https://github.com/user-attachments/assets/1774e023-6660-44b3-9216-c30282cfb891" />

OBS: na IAinteracoes e tambem na Evolucoes, foi visto que excluir nao faria muito sentido no contexto


##  Functions

As Functions (Funções) são rotinas SQL que retornam valores ou tabelas e são utilizadas para realizar cálculos, consultas reutilizáveis e regras específicas diretamente no banco de dados.

Diferente das Stored Procedures, as Functions:
- Sempre retornam um valor ou conjunto de dados
- Podem ser utilizadas dentro de SELECTs
- Não realizam operações de INSERT, UPDATE ou DELETE

No projeto ACADIA, as Functions são utilizadas principalmente para:
- Cálculos relacionados à evolução física do usuário
- Consolidação de dados para relatórios
- Apoio a consultas utilizadas por dashboards
- Centralização de regras de cálculo no banco de dados

O uso de Functions melhora a organização do código, evita repetição de lógica e facilita a manutenção do sistema.


Usuarios

<img width="127" height="57" alt="image" src="https://github.com/user-attachments/assets/85e2cdf0-52c9-47dc-be49-bdae76d3280a" />

Exercicio

<img width="157" height="40" alt="image" src="https://github.com/user-attachments/assets/d7ae95ac-c086-46b1-9b6e-cb640d4b77ff" />

Treino 

<img width="161" height="77" alt="image" src="https://github.com/user-attachments/assets/1f7a71ea-ae10-4cc2-9a17-deafcb58f45d" />

Evolucoes 

<img width="185" height="261" alt="image" src="https://github.com/user-attachments/assets/917a7f74-b1b7-473f-89b5-b9fafde9de2c" />

Treino Exercicio e IAinteracoes nao tem registro de Functions


##  Views

As Views são consultas SQL salvas no banco de dados que representam uma visualização lógica dos dados, combinando informações de uma ou mais tabelas.

No projeto ACADIA, as Views são utilizadas para:
- Simplificar consultas complexas
- Centralizar joins entre tabelas relacionadas
- Facilitar a leitura de dados pelo sistema
- Apoiar relatórios e dashboards
- Garantir padronização na forma como os dados são consultados

As Views não armazenam dados físicos, apenas refletem os dados das tabelas, contribuindo para uma arquitetura mais organizada, legível e de fácil manutenção.

Usuario

<img width="175" height="76" alt="image" src="https://github.com/user-attachments/assets/a07802d0-f307-4d8b-ad02-436bc77300a0" />

Exercicio

<img width="234" height="59" alt="image" src="https://github.com/user-attachments/assets/350d29f5-36cc-4dbd-8ab3-93574f011d17" />

Treino

<img width="179" height="81" alt="image" src="https://github.com/user-attachments/assets/89c07378-bd32-43eb-bcfd-6b69012d240c" />


TreinoExercicio

<img width="177" height="38" alt="image" src="https://github.com/user-attachments/assets/bd033e56-276f-438c-af05-9c4e99b6e392" />


IAintereacoes

<img width="158" height="38" alt="image" src="https://github.com/user-attachments/assets/2663e7b0-f441-4fc2-bf53-79db6fce362f" />


Evolucoes

<img width="147" height="59" alt="image" src="https://github.com/user-attachments/assets/bd6b9a87-a149-499e-bd55-f23a0ab03f7f" />

----

### Exemplo de chamada no Swagger

Usuario/Obter

<img width="1273" height="906" alt="image" src="https://github.com/user-attachments/assets/67e65161-1526-4c85-8208-9e9c0faa554c" />

Todos as implementações 

<img width="1257" height="707" alt="image" src="https://github.com/user-attachments/assets/934f6751-01ee-4a19-b2db-fed9c572fe38" />
<img width="1150" height="706" alt="image" src="https://github.com/user-attachments/assets/1635a8e6-b52f-45c2-8fd0-a745728eedbf" />
<img width="1177" height="610" alt="image" src="https://github.com/user-attachments/assets/7edf13fe-6c97-406b-af32-dd30295152a8" />
<img width="1237" height="325" alt="image" src="https://github.com/user-attachments/assets/3f7bc251-a6be-44bd-8875-6adcf9b0d111" />




##  Arquitetura do Sistema

O backend do projeto ACADIA foi desenvolvido seguindo uma arquitetura em camadas, com separação clara de responsabilidades entre os projetos da solução.

A solução é composta pelos seguintes projetos:

- ProjetoBackend.API  
Responsável por expor a API REST. Contém os Controllers, configuração de rotas, autenticação com JWT e inicialização da aplicação. É o ponto de entrada das requisições do frontend.

- ProjetoBackend.Aplicacao  
Camada responsável por orquestrar os casos de uso do sistema. Faz a comunicação entre a API e as camadas de domínio, repositório e serviços externos.

- ProjetoBackend.Dominio  
Contém as entidades do sistema e as regras de negócio principais. Representa o núcleo da aplicação, sem dependência de frameworks ou infraestrutura.

- ProjetoBackend.Repositorio  
Responsável pelo acesso ao banco de dados. Executa Stored Procedures, Functions e Views utilizando Dapper, garantindo performance e controle total das consultas SQL.

- ProjetoBackend.Services  
Camada responsável pela integração com serviços externos, como a API de Inteligência Artificial utilizada para gerar interações inteligentes com o usuário.

Essa arquitetura facilita a manutenção, organização do código, escalabilidade do sistema e permite que cada camada evolua de forma independente.


## Inteligencia Artificial 

Chatbot interativo voltado para treino, saúde e nutrição, oferecendo suporte ao usuário com orientações e acompanhamento para melhorar resultados e manter consistência.

## ☁️ Deploy

| Componente | Onde | Região |
| --- | --- | --- |
| API .NET 8 | Railway (Docker) | US West — Califórnia |
| Frontend React/Vite | Vercel | CDN global |
| Banco PostgreSQL | Supabase | us-west-2 — Oregon |

A região da API acompanha a do banco: o gargalo é a conversa API↔banco, que
acontece várias vezes por request, e não navegador↔banco.

### Railway

O `Dockerfile` na raiz é multi-stage (SDK 8.0 para build, ASP.NET 8.0 para
runtime) e roda como usuário sem privilégios. Pontos a observar:

- **Porta** — o Railway injeta `PORT` e faz health check nela. O `Program.cs` lê a
  variável e faz o binding; nada a configurar.
- **Proxy** — o container recebe HTTP puro. O `UseForwardedHeaders` recupera o
  esquema e o IP real do cliente; sem isso o HTTPS redirect entra em loop e todo o
  tráfego anônimo cairia numa única partição do rate limiter.
- **Migrations** — aplicadas na subida da aplicação, então o deploy leva código e
  schema juntos.
- **Network Restrictions do Supabase não são viáveis**: exigem IP de saída fixo,
  exclusivo do plano Pro do Railway.

Cadastre as variáveis da seção [Configuração](#️-configuração) antes do primeiro deploy.

### Vercel

Defina `VITE_API_URL` como `https://<seu-app>.up.railway.app/api` (ver
`.env.example` no repositório do frontend). O Vite lê variáveis `VITE_*` **no
build**, não em tempo de execução: alterar o valor exige um novo deploy.

Depois, adicione o domínio da Vercel em `Cors__Origens__0` no Railway.

## Melhorias Futuras

- Execução de treino em tempo real  
Implementação de um sistema de execução de treino, onde o usuário poderá selecionar um treino e iniciá-lo diretamente na aplicação. O sistema contará com cronômetro de descanso, contador de séries e repetições, oferecendo suporte ativo durante a realização do treino.

- Compartilhamento de treinos  
Funcionalidade que permitirá ao usuário compartilhar seus treinos com amigos ou colegas, possibilitando que outras pessoas utilizem exatamente o mesmo plano de treino.

- Sistema de recompensas e conquistas  
Implementação de um sistema de conquistas e recompensas, onde o usuário será incentivado a manter a constância nos treinos e na evolução corporal. Conquistas poderão ser desbloqueadas ao completar ações específicas, como concluir treinos, registrar evoluções ou interagir com a IA.

Essas melhorias visam aumentar o engajamento do usuário, melhorar a experiência durante os treinos e expandir as funcionalidades do sistema.
