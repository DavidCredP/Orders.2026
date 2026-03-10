using Microsoft.AspNetCore.Mvc;
using Order.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;

namespace Order.Backend.Controllers;

[Route("api/[controller]")]

[ApiController]
public class CategoriesController : GenericController<Category>
{
    public CategoriesController(IGenericUnitOfWork<Category> unitOfWork) : base(unitOfWork)
    {
    }
}