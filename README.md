
# 📋 TaskManager — Sistema de Gerenciamento de Tarefas

Projeto **TaskManager** — aplicação completa de gerenciamento de tarefas (To-Do App) com **Angular 15** no frontend e **ASP.NET Core Web API** no backend. Este repositório contém instruções rápidas para configurar, executar e entender a arquitetura do sistema.

---

## 🚀 Tecnologias utilizadas

**Frontend**
- Angular 15.2.0  
- Bootstrap 5.3.0  
- Bootstrap Icons 1.11.0  
- TypeScript 4.8.0  
- RxJS 7.8.0

**Backend**
- ASP.NET Core 6.0 / 7.0 (API RESTful)  
- Entity Framework Core (ORM)  
- SQL Server (Banco de dados)  
- Swagger (Documentação da API)

---

## 🧭 Visão geral do sistema

O TaskManager permite criação, edição, listagem e remoção de tarefas com campos típicos (título, descrição, prioridade, data de vencimento, status). O frontend em Angular consome a API REST criada em ASP.NET Core. O backend utiliza EF Core para persistência em SQL Server e expõe endpoints documentados por Swagger.

---

## ✨ Principais funcionalidades
- CRUD de tarefas (Create, Read, Update, Delete)  
- Filtros por status e prioridade  
- Ordenação por data de vencimento e prioridade  
- Validações no frontend e backend  
- Documentação da API via Swagger

---

## 🧩 Estrutura sugerida do repositório

```
/taskmanager
  /frontend/          # projeto Angular
  /backend/           # projeto ASP.NET Core Web API
  README.md
```

---

## 🛠️ Pré-requisitos

- Node.js (v16+ recomendado)
- npm ou yarn
- Angular CLI (opcional, para desenvolvimento local)
- .NET SDK 6.0 ou 7.0
- SQL Server (local ou remoto)
- dotnet-ef (para migrations, opcional: `dotnet tool install --global dotnet-ef`)

---

## 🚧 Configuração do Backend (ASP.NET Core)

1. Abra o diretório do backend:
```bash
cd backend
```

2. Configure a string de conexão em `appsettings.json` (exemplo):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=TaskManagerDb;Trusted_Connection=True;"
  },
  "Logging": { "LogLevel": { "Default": "Information" } },
  "AllowedHosts": "*"
}
```

3. (Opcional) Crie/Migre o banco com EF Core:
```bash
# gerar migration
dotnet ef migrations add InitialCreate

# aplicar migrations
dotnet ef database update
```

4. Executar a API:
```bash
dotnet run
```

5. A API normalmente ficará disponível em:
```
https://localhost:5001
http://localhost:5000
```
E a documentação do Swagger em:
```
https://localhost:5001/swagger
```

---

## 🚀 Configuração do Frontend (Angular)

1. Abra o diretório do frontend:
```bash
cd frontend
```

2. Instale dependências:
```bash
npm install
# ou
yarn
```

3. Configure a URL base da API no arquivo de ambientes:
- `src/environments/environment.ts`
```ts
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001/api' // ajuste conforme sua API
};
```

4. Executar em modo de desenvolvimento:
```bash
ng serve --open
```
A aplicação Angular abrirá em `http://localhost:4200`.

---

## 📦 Exemplo rápido de endpoints (API)

- `GET /api/tasks` — listar tarefas  
- `GET /api/tasks/{id}` — obter tarefa por id  
- `POST /api/tasks` — criar tarefa  
- `PUT /api/tasks/{id}` — atualizar tarefa  
- `DELETE /api/tasks/{id}` — deletar tarefa

---

## 💡 Boas práticas / sugestões

- Habilitar CORS apenas para origens necessárias no ambiente de produção.  
- Usar DTOs (Data Transfer Objects) no backend para separar entidades do modelo de persistência.  
- Paginação e filtros na listagem de tarefas para performance.  
- Tratamento de erros consistente (middleware de exception handling).  
- Adicionar testes unitários (xUnit / Jest) e testes de integração.

---

## 🧪 Testes

- Backend: recomenda-se xUnit + Moq para unit tests e testes de integração com InMemoryDatabase do EF Core.  
- Frontend: usar Jasmine/Karma ou Jest para testes unitários de componentes e serviços.

---

## 🤝 Como contribuir

1. Fork deste repositório.  
2. Crie uma branch com a feature/bugfix: `git checkout -b feature/nome-da-feature`.  
3. Faça commits claros e atômicos.  
4. Abra um Pull Request descrevendo as mudanças.

---

## 📜 Licença

Este projeto pode usar a licença que preferir. Exemplo: MIT License.

```
MIT License
© 2025 Seu Nome
```

---

## 📞 Contato

Se quiser, adicione informações de contato (email, LinkedIn) ou instruções adicionais para deploy/CI.
