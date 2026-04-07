using Order.Backend.Repositories.Interfaces;
using Order.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Order.Backend.UnitsOfWork.Implementations;

public class OrdersUnitOfWork : GenericUnitOfWor<Orden>, IOrdersUnitOfWork
{
    private readonly IOrdersRepository _ordersRepository;

    public OrdersUnitOfWork(IGenericRepository<Orden> repository, IOrdersRepository ordersRepository) : base(repository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<ActionResponse<IEnumerable<Orden>>> GetAsync(string email, PaginationDTO pagination) => await _ordersRepository.GetAsync(email, pagination);

    public async Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination) => await _ordersRepository.GetTotalPagesAsync(email, pagination);

    public async Task<ActionResponse<Orden>> UpdateFullAsync(string email, OrderDTO orderDTO) => await _ordersRepository.UpdateFullAsync(email, orderDTO);

    public override async Task<ActionResponse<Orden>> GetAsync(int id) => await _ordersRepository.GetAsync(id);
}