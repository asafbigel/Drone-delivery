using Microsoft.AspNetCore.Mvc;
using BlWebApi.Models;
using BlWebApi.Services;
using BlApi;

namespace BlWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseStationsController : ControllerBase
{
    private readonly IBL _bl;
    private readonly ApiMapper _map;
    public BaseStationsController(IBL bl, ApiMapper map) { _bl = bl; _map = map; }

    [HttpGet]
    public ActionResult<IEnumerable<BaseStationToListDto>> GetAll()
    {
        var res = _bl.GetAllBaseStations(_ => true).Select(_map.ToDto).ToList();
        return Ok(res);
    }

    [HttpGet("{id:int}")]
    public ActionResult<BaseStationDetailsDto> GetOne(int id)
    {
        var b = _bl.GetBaseStation(id);
        return Ok(_map.ToDto(b));
    }

    [HttpPatch("{id:int}")]
    public IActionResult Update(int id, UpdateBaseStationRequest req)
    {
        _bl.UpdateBaseStation(id, req.Name ?? "", req.ChargingSlots?.ToString() ?? "");
        return NoContent();
    }
}
