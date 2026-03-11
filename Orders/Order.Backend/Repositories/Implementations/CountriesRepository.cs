using Microsoft.EntityFrameworkCore;
using Order.Backend.Data;
using Order.Backend.Helpers;
using Order.Backend.Repositories.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Order.Backend.Repositories.Implementations;

public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
{
    private readonly DataContext _context;

    public CountriesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Countries.Include(c => c.States).AsQueryable();

        return new ActionResponse<IEnumerable<Country>>
        {
            WasSuccess = true,
            Result = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync()
        };
    }

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
    {
        var contries = await _context.Countries.Include(c => c.States).ToListAsync();
        return new ActionResponse<IEnumerable<Country>>
        {
            WasSuccess = true,
            Result = contries
        };
    }

    public override async Task<ActionResponse<Country>> GetAsync(int id)
    {
        var country = await _context.Countries.Include(c => c.States!).ThenInclude(x => x.Cities).FirstOrDefaultAsync(c => c.Id == id);
        if (country == null)
        {
            return new ActionResponse<Country>
            {
                Message = "Registro no encontrado"
            };
        }
        return new ActionResponse<Country>
        {
            WasSuccess = true,
            Result = country
        };
    }
}