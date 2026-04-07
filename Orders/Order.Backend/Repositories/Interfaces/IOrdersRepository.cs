using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Order.Backend.Repositories.Interfaces;

public interface IOrdersRepository
{
    Task<ActionResponse<IEnumerable<Orden>>> GetAsync(string email, PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination);

    Task<ActionResponse<Orden>> GetAsync(int id);

    Task<ActionResponse<Orden>> UpdateFullAsync(string email, OrderDTO orderDTO);
}