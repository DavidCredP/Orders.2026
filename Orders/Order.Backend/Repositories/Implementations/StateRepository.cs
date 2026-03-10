using Microsoft.EntityFrameworkCore;
using Order.Backend.Data;
using Order.Backend.Repositories.Interfaces;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Order.Backend.Repositories.Implementations;

public class StateRepository : GenericRepository<State>, IStatesRepository
{
    private readonly DataContext _context;

    public StateRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<IEnumerable<State>>> GetAsync()
    {
        var states = await _context.States.Include(s => s.Cities).ToListAsync();
        return new ActionResponse<IEnumerable<State>>
        {
            WasSuccess = true,
            Result = states
        };
    }

    public override async Task<ActionResponse<State>> GetAsync(int id)
    {
        var states = await _context.States.Include(s => s.Cities).FirstOrDefaultAsync(s => s.StateId == id);
        if (states == null)
        {
            return new ActionResponse<State>
            {
                Message = "Estado no encontrado"
            };
        }
        return new ActionResponse<State>
        {
            WasSuccess = true,
            Result = states
        };
    }
}