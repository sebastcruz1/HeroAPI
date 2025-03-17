using HeroAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)

{
    public DbSet<Heros> Hero { get; set; }
}