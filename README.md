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



---
## 🎮 Como interagir com o Agente (Guia de Comandos)

## 🧪 Testes Técnicos via Scalar (Manual CRUD)

Se você não estiver usando o Claude Desktop, pode testar todas as funcionalidades da API através da interface do **Scalar**. 
Acesse: `http://localhost:5225/scalar/v1`

Selecione o endpoint **POST `/mcp/executar`** e utilize os JSONs abaixo no corpo (Body) da requisição:

| Operação | `toolName` | `arguments` (Exemplo) | Objetivo |
| :--- | :--- | :--- | :--- |
| **Listar** | `listar_clientes` | `{}` | Ver todos os clientes cadastrados. |
| **Criar** | `criar_cliente` | `{"nome": "Novo Cliente", "email": "novo@teste.com"}` | Adicionar um registro ao banco. |
| **Editar** | `editar_cliente` | `{"id": 1, "status": "VIP", "nome": "Nome Editado"}` | Atualizar dados de um ID específico. |
| **Excluir** | `excluir_cliente` | `{"id": 2}` | Remover um cliente permanentemente. |

### 🛠️ Exemplo de JSON para Copiar e Colar no Scalar:

**Para Criar:**
```json
{
  "toolName": "criar_cliente",
  "arguments": {
    "nome": "Ana Scalar",
    "email": "ana.scalar@exemplo.com"
  }
}
Para Editar:

JSON

{
  "toolName": "editar_cliente",
  "arguments": {
    "id": 1,
    "status": "Ativo - Editado via Scalar"
  }
}
