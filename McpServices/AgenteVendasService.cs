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


    [Description("Atualiza os dados de um cliente existente.")]
    public async Task<string> EditarCliente(
    [Description("O ID do cliente que será editado")] int id,
    [Description("O novo nome do cliente (opcional)")] string? nome = null,
    [Description("O novo e-mail do cliente (opcional)")] string? email = null,
    [Description("O novo status (opcional)")] string? status = null)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return $"Erro: Cliente {id} não encontrado.";

        // Atualiza apenas o que foi enviado
        if (!string.IsNullOrEmpty(nome)) cliente.Nome = nome;
        if (!string.IsNullOrEmpty(email)) cliente.Email = email;
        if (!string.IsNullOrEmpty(status)) cliente.Status = status;

        await _context.SaveChangesAsync();
        return $"Sucesso! Cliente {id} atualizado.";
    }


    [Description("Exclui um cliente do banco de dados permanentemente usando o ID.")]
    public async Task<string> ExcluirCliente(
    [Description("O ID numérico do cliente que deve ser excluído")] int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
            return $"Erro: Cliente com ID {id} não encontrado.";

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return $"Sucesso! O cliente '{cliente.Nome}' (ID: {id}) foi removido do sistema.";
    }
}