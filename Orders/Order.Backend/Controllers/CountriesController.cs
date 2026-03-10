using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Backend.Data;
using Order.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;
using System.Diagnostics.Metrics;

namespace Order.Backend.Controllers;

[Route("api/[controller]")]

[ApiController]
public class CountriesController : GenericController<Country>
{
    public CountriesController(IGenericUnitOfWork<Country> unitOfWork) : base(unitOfWork)
    {
    }
}