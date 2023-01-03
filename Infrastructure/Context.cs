using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<Buy> Buy { get; set; }
}