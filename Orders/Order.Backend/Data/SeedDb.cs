using Microsoft.EntityFrameworkCore;
using Orders.Shared.Entities;

namespace Order.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedDbAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckContriesFullAsync();
        await CheckCountriesAsync();
        await CheckCategoriesAsync();
    }

    private async Task CheckContriesFullAsync()
    {
        if (!_context.Countries.Any())
        {
            var countriesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }

    private async Task CheckCategoriesAsync()
    {
        if (!_context.Categories.Any())
        {
            _context.Categories.Add(new Category { Name = "Calzado" });
            _context.Categories.Add(new Category { Name = "Tecnologia" });
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.Categories.Any())
        {
            _context.Countries.Add(new Country { Name = "Colombia" });
            _context.Countries.Add(new Country { Name = "Mexico" });
            await _context.SaveChangesAsync();
        }
    }
}