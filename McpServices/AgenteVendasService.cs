using System.ComponentModel;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MeuAgenteMcp.Models;

namespace MeuAgenteMcp.McpServices;

public class AgenteVendasService
{
    private readonly AppDbContext _context;

    public AgenteVendasService(AppDbContext context)
    {
        _context = context;
    }

    [Description("Retorna a lista de todos os clientes e seus status atuais.")]
    public async Task<string> ListarClientes()
    {
        var clientes = await _context.Clientes.ToListAsync();
        return JsonSerializer.Serialize(clientes);
    }


    // --- NOVA FERRAMENTA AQUI ---
    [Description("Cadastra um novo cliente no banco de dados.")]
    public async Task<string> CriarCliente(
        [Description("O nome completo do cliente")] string nome,
        [Description("O e-mail válido do cliente")] string email)
    {
        var novoCliente = new Cliente { Nome = nome, Email = email, Status = "Ativo" };

        _context.Clientes.Add(novoCliente);
        await _context.SaveChangesAsync();

        return $"Sucesso! Cliente {nome} foi cadastrado com o ID {novoCliente.Id}.";
    }
}