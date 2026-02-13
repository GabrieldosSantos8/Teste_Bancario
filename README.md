# 🏦 Teste Itau - API de Operações Financeiras

![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)

API desenvolvida para simular o controle de operações de ativos financeiros, permitindo a gestão de compra e venda de ativos e consulta de posições.

## 🛠️ Tecnologias e Boas Práticas
- **ASP.NET Core 8.0** (Web API)
- **Entity Framework Core** (ORM)
- **Migrations** para versionamento de banco de dados
- **Swagger/OpenAPI** para documentação interativa
- **Programação Assíncrona** (Async/Await)

## 🏗️ Arquitetura
O projeto segue o padrão MVC (Model-View-Controller) focado em API, com separação clara de responsabilidades:
- **Models:** Definição das entidades e regras de banco (Data Annotations).
- **Controllers:** Exposição dos endpoints REST.
- **Data:** Contexto do Entity Framework e mapeamento de tabelas.

## ⚙️ Como Executar
1. **Configurar o Banco:** No arquivo `appsettings.json`, ajuste a sua ConnectionString com suas credenciais do MySQL.
2. **Migrações:**
   Execute o comando abaixo para criar as tabelas no seu banco:
   ```bash
   dotnet ef database update
