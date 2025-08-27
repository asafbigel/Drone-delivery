using Microsoft.AspNetCore.Mvc;
using BlWebApi.Models;
using BlWebApi.Services;
using BlApi;

namespace BlWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IBL _bl;
    private readonly ApiMapper _map;
    public CustomersController(IBL bl, ApiMapper map) { _bl = bl; _map = map; }

    [HttpGet]
    public ActionResult<IEnumerable<CustomerToListDto>> GetAll()
    {
        var res = _bl.GetAllCustomers(_ => true).Select(_map.ToDto).ToList();
        return Ok(res);
    }

    [HttpGet("{id:int}")]
    public ActionResult<CustomerDetailsDto> GetOne(int id)
    {
        var c = _bl.GetCustomer(id);
        return Ok(_map.ToDto(c));
    }

    [HttpPatch("{id:int}")]
    public IActionResult Update(int id, UpdateCustomerRequest req)
    {
        _bl.UpdateCustomer(id, req.Name ?? "", req.Phone ?? "", req.Password ?? "", req.Latitude ?? 0, req.Longitude ?? 0);
        return NoContent();
    }
}
