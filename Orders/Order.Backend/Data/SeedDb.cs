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
        await CheckCountriesAsync();
        await CheckCategoriesAsync();
    }

    private async Task CheckCategoriesAsync()
    {
        if (!_context.Categories.Any())
        {
            _context.Categories.Add(new Category { CategoryName = "Calzado" });
            _context.Categories.Add(new Category { CategoryName = "Tecnologia" });
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.Categories.Any())
        {
            _context.Countries.Add(new Country { CountryName = "Colombia" });
            _context.Countries.Add(new Country { CountryName = "Mexico" });
            await _context.SaveChangesAsync();
        }
    }
}