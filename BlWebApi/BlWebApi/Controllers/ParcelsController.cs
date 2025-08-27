using Microsoft.AspNetCore.Mvc;
using BlWebApi.Models;
using BlWebApi.Services;
using BlApi;

namespace BlWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParcelsController : ControllerBase
{
    private readonly IBL _bl;
    private readonly ApiMapper _map;
    public ParcelsController(IBL bl, ApiMapper map) { _bl = bl; _map = map; }

    [HttpGet]
    public ActionResult<IEnumerable<ParcelToListDto>> GetAll()
    {
        var res = _bl.GetAllParcels(_ => true).Select(_map.ToDto).ToList();
        return Ok(res);
    }

    [HttpGet("{id:int}")]
    public ActionResult<ParcelDetailsDto> GetOne(int id)
    {
        var p = _bl.GetParcel(id);
        return Ok(_map.ToDto(p));
    }

    [HttpPost]
    public ActionResult Create(CreateParcelRequest req)
    {
        var bo = new BO.Parcel
        {
            Sender = new BO.CustomerAtParcel { Id = req.SenderId },
            Getter = new BO.CustomerAtParcel { Id = req.ReceiverId },
            Weight = Enum.Parse<BO.WeightCategories>(req.Weight.ToString())
        };
        var id = _bl.AddParcel(bo);
        return CreatedAtAction(nameof(GetOne), new { id }, new { id });
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _bl.DeleteParcel(id);
        return NoContent();
    }
}
