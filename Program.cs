using MeuAgenteMcp.McpServices;
using MeuAgenteMcp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ClientesDb"));
builder.Services.AddScoped<AgenteVendasService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Gera o documento json
    app.MapScalarApiReference(); // Cria a interface em /scalar/v1
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Clientes.Add(new Cliente { Nome = "André MCP", Email = "andre@teste.com", Status = "Ativo" });
    db.Clientes.Add(new Cliente { Nome = "João IA", Email = "joao@teste.com", Status = "Inativo" });
    db.SaveChanges();
}

// Endpoints existentes...
app.MapGet("/mcp/tools", () => {
    return new[] {
        new { name = "listar_clientes", description = "Busca todos os clientes no banco" },
        new { name = "criar_cliente", description = "Cadastra um novo cliente" },
        new { name = "editar_cliente", description = "Atualiza nome, email ou status de um cliente" },
        new { name = "excluir_cliente", description = "Remove um cliente pelo ID" }
    };
});

app.MapPost("/mcp/executar", async (JsonElement body, AgenteVendasService service) => {
    // 1. Extrai os dados do JSON recebido
    if (!body.TryGetProperty("toolName", out var toolProperty))
    {
        return Results.BadRequest("O campo 'toolName' é obrigatório.");
    }

    string toolName = toolProperty.GetString() ?? "";

    // 2. Executa a lógica baseada no nome
    if (toolName == "listar_clientes")
    {
        var resultado = await service.ListarClientes();
        return Results.Ok(resultado);
    }

    if (toolName == "criar_cliente")
    {
        var args = body.GetProperty("arguments");
        var nome = args.GetProperty("nome").GetString();
        var email = args.GetProperty("email").GetString();
        var resultado = await service.CriarCliente(nome!, email!);
        return Results.Ok(resultado);
    }

    if (toolName == "editar_cliente")
    {
        var args = body.GetProperty("arguments");
        var id = args.GetProperty("id").GetInt32();

        string? nome = args.TryGetProperty("nome", out var n) ? n.GetString() : null;
        string? email = args.TryGetProperty("email", out var e) ? e.GetString() : null;
        string? status = args.TryGetProperty("status", out var s) ? s.GetString() : null;

        return Results.Ok(await service.EditarCliente(id, nome, email, status));
    }

    if (toolName == "excluir_cliente")
    {
        var args = body.GetProperty("arguments");
        var id = args.GetProperty("id").GetInt32();
        return Results.Ok(await service.ExcluirCliente(id));
    }

    return Results.NotFound($"Ferramenta '{toolName}' não encontrada.");
});

app.MapGet("/", () => Results.Redirect("/scalar/v1"));

app.Run();