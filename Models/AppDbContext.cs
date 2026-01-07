using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MeuAgenteMcp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Cliente> Clientes => Set<Cliente>();
}