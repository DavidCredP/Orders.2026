using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Order.Backend.UnitsOfWork.Interfaces;

public interface IOrdersUnitOfWork : IGenericUnitOfWork<Orden>
{
    Task<ActionResponse<IEnumerable<Orden>>> GetAsync(string email, PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination);

    Task<ActionResponse<Orden>> GetAsync(int id);

    Task<ActionResponse<Orden>> UpdateFullAsync(string email, OrderDTO orderDTO);

    Task<ActionResponse<Orden>> AddAsync(Orden order);
}