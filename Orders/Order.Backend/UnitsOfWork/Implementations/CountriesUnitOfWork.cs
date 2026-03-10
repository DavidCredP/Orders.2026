using Order.Backend.Repositories.Interfaces;
using Order.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Order.Backend.UnitsOfWork.Implementations;

public class CountriesUnitOfWork : GenericUnitOfWor<Country>, ICountriesUnitOfWork
{
    private readonly ICountriesRepository _countriesRepository;

    public CountriesUnitOfWork(IGenericRepository<Country> repository, ICountriesRepository countriesRepository) : base(repository)
    {
        _countriesRepository = countriesRepository;
    }

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
    {
        return await _countriesRepository.GetAsync();
    }

    public override async Task<ActionResponse<Country>> GetAsync(int id)
    {
        return await _countriesRepository.GetAsync(id);
    }
}