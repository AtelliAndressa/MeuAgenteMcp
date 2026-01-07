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
}