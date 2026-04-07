using Orders.Shared.Responses;

namespace Order.Backend.Helpers;

public interface IOrdersHelper
{
    Task<ActionResponse<bool>> ProcessOrderAsync(string email, string remarks);
}