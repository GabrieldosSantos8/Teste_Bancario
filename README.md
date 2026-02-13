# 🏦 Teste Itau - API de Operações Financeiras

![.NET](https://img.shields.io/badge/.NET-8.0-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)

API desenvolvida para simular o controle de operações de ativos financeiros, permitindo a gestão de compra e venda e a consulta de posições consolidadas.

## 🛠️ Tecnologias e Boas Práticas
- **ASP.NET Core 8.0** (Web API)
- **Entity Framework Core** (ORM)
- **Migrations** para versionamento de banco de dados
- **Swagger/OpenAPI** para documentação e testes de endpoints
- **Programação Assíncrona** (Async/Await)

## 🏗️ Arquitetura
O projeto utiliza a estrutura padrão do .NET com separação de responsabilidades:
- **Models:** Entidades do banco de dados com Data Annotations.
- **Controllers:** Endpoints REST para operações de CRUD.
- **Data:** Contexto do Entity Framework (`AppDbContext`) e mapeamento de tabelas.

## ⚙️ Como Executar

1. **Clonar o Repositório:**
   ```bash
   git clone [https://github.com/GabrieldosSantos8/TesteItau.git](https://github.com/GabrieldosSantos8/TesteItau.git)
