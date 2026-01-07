using MeuAgenteMcp.McpServices;
using MeuAgenteMcp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Banco de dados em memória para teste rápido
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ClientesDb"));

// Registra nosso serviço
builder.Services.AddScoped<AgenteVendasService>();

var app = builder.Build();

// Criando dados de teste (Seed)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Clientes.Add(new Cliente { Nome = "André MCP", Email = "andre@teste.com", Status = "Ativo" });
    db.Clientes.Add(new Cliente { Nome = "João IA", Email = "joao@teste.com", Status = "Inativo" });
    db.SaveChanges();
}

// Endpoint que o Claude vai acessar
app.MapGet("/mcp/tools", () => {
    return new[] {
        new { name = "listar_clientes", description = "Busca todos os clientes no banco" },
        new { name = "criar_cliente", description = "Cadastra um novo cliente" }
    };
});

app.MapPost("/mcp/executar", async (string toolName, JsonElement arguments, AgenteVendasService service) => {
    if (toolName == "listar_clientes")
        return await service.ListarClientes();

    if (toolName == "criar_cliente")
    {
        var nome = arguments.GetProperty("nome").GetString();
        var email = arguments.GetProperty("email").GetString();
        return await service.CriarCliente(nome!, email!);
    }

    return "Ferramenta não encontrada";
});

app.MapGet("/", () => "Agente MCP .NET 10 está online e operante!");

app.Run();