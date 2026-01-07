# 🤖 MeuAgenteMcp - AI Engineer .NET 10

Este projeto é uma implementação de ponta de um **Agente de IA** utilizando o protocolo **MCP (Model Context Protocol)** integrado ao ecossistema **.NET 10**. O objetivo é permitir que modelos de linguagem (como o Claude) interajam diretamente com dados de negócio armazenados no SQL Server de forma segura e inteligente.



## 🌟 Diferenciais deste Projeto
- **Cutting Edge:** Desenvolvido utilizando o **.NET 10.0.1 (Preview)**.
- **ORM Moderno:** Uso de **Entity Framework Core** para abstração de dados.
- **Protocolo MCP:** Integração nativa para que a IA consuma ferramentas (Tools) da API.
- **Banco de Dados em Memória:** Configurado para testes rápidos e demonstração (In-Memory).

## 🛠️ Tecnologias e Pacotes
- `Microsoft.Extensions.AI`: O novo padrão da Microsoft para integração de IA.
- `Microsoft.EntityFrameworkCore.InMemory`: Para simulação de banco de dados SQL.
- `DarBotLabs.PowerAgent.MCP`: SDK para facilitação do protocolo MCP em C#.

---

## 🚀 Como Executar

### 1. Pré-requisitos
- .NET 10 SDK instalado.
- Visual Studio 2022 (com Preview Features habilitado).

### 2. Rodar a API
No terminal do projeto, execute:
```bash
dotnet run
