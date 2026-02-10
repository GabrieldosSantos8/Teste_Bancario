# TesteItau - API RESTful .NET Core

## Visão Geral

Esta aplicação é uma API RESTful desenvolvida em ASP.NET Core para gestão de ativos, cotações, operações, posições e usuários, integrando-se a um banco de dados MySQL. Possui endpoints para CRUD, consultas analíticas, integração com API externa da B3 e está preparada para integração com microserviços via Kafka.

## Principais Funcionalidades

- CRUD completo para Ativos, Cotações, Operações, Posições e Usuários
- Endpoints analíticos:
  - Última cotação de um ativo
  - Preço médio por ativo para um usuário
  - Posição de um cliente
  - Valor total de corretagem
  - Top 10 clientes por posição e por corretagem
- Integração com API pública da B3 para atualização de cotações
- Pronto para integração com Kafka (consumo de cotações via Worker Service)
- Swagger UI para testes e documentação interativa
- CORS habilitado para integração com frontends (ex: Angular)

## Estrutura das Tabelas

- **tb_ativos**: pk_ativo, ds_codigo, ds_nome
- **tb_cotacao**: pk_cotacao, fk_ativo, vl_preco_unitario, dt_cotacao
- **tb_operacoes**: pk_operacao, fk_usuario, fk_ativo, nr_quantidade, vl_preco_unitario, tp_operacao, vl_corretagem, dt_operacao
- **tb_posicao**: pk_posicao, fk_usuario, fk_ativo, nr_quantidade_total, vl_preco_medio, vl_lucro_prejuizo
- **tb_usuarios**: pk_usuario, ds_nome, ds_email, vl_perc_corretagem

## Endpoints Principais

### Ativos
- `GET /api/ativos/ObterTodos` — Lista todos os ativos
- `GET /api/ativos/UltimaCotacao/{codigoAtivo}` — Última cotação do ativo
- `GET /api/ativos/PrecoMedioPorAtivo/{usuarioId}/{codigoAtivo}` — Preço médio do ativo para o usuário
- `GET /api/ativos/PosicaoCliente/{usuarioId}` — Posição do cliente
- `GET /api/ativos/ValorTotalCorretagem` — Valor total de corretagem
- `GET /api/ativos/Top10ClientesMaiorPosicao` — Top 10 clientes por posição
- `GET /api/ativos/Top10ClientesMaisCorretagem` — Top 10 clientes por corretagem

### Cotações
- `POST /api/cotacoes/ObterCotacoes` — Atualiza cotações via API B3
- `GET /api/cotacoes/ObterTodos` — Lista todas as cotações

### Operações, Posições, Usuários
- `GET /api/operacoes/ObterTodos`
- `GET /api/posicoes/ObterTodos`
- `GET /api/usuarios/ObterTodos`

## Integração Externa

- **API B3**: Consome cotações de ativos via https://b3api.vercel.app/api/Assets/
- **Kafka (planejado)**: Worker Service para consumir cotações de microserviço externo, com retry, idempotência, circuit breaker e fallback.

## Observabilidade e Resiliência

- Estratégias de retry e idempotência para consumo de cotações
- Circuit breaker e fallback para garantir funcionamento do serviço de operações mesmo com falha no serviço de cotações
- Logs e métricas recomendados via Application Insights ou Prometheus/Grafana

## Testes

- Testes unitários com xUnit em `Tests/OperacaoTests.cs`
- Testes positivos (valores esperados) e negativos (quantidade zero, listas vazias)

## Como Executar

1. Configure a string de conexão MySQL em `appsettings.json`
2. Execute as migrations (se necessário)
3. Rode a aplicação:
   ```
   dotnet run
   ```
4. Acesse o Swagger em http://localhost:5000/

## Observações

- CORS habilitado para qualquer origem
- Endpoints RESTful seguindo boas práticas
- Pronto para evolução com microserviços e mensageria

---

> Para dúvidas ou contribuições, consulte o código-fonte ou entre em contato com o responsável pelo projeto.
