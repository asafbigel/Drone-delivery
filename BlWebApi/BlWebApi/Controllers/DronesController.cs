using Microsoft.AspNetCore.Mvc;
using BlWebApi.Models;
using BlWebApi.Services;
using BlApi;
using BO;

namespace BlWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DronesController : ControllerBase
{
    private readonly IBL _bl;
    private readonly ApiMapper _map;
    public DronesController(IBL bl, ApiMapper map) { _bl = bl; _map = map; }

    [HttpGet]
    public ActionResult<IEnumerable<DroneToListDto>> GetAll([FromQuery] DroneStatus? status, [FromQuery] WeightCategory? weight)
    {
        Predicate<DroneToList> pred = _ => true;
        if (status is not null) pred = d => d.Status.ToString() == status.ToString();
        if (weight is not null)
        {
            var prev = pred;
            pred = d => prev(d) && d.MaxWeight.ToString() == weight.ToString();
        }
        var res = _bl.GetAllDrones(pred).Select(_map.ToDto).ToList();
        return Ok(res);
    }

    [HttpGet("{id:int}")]
    public ActionResult<DroneDetailsDto> GetOne(int id)
    {
        var dl = _bl.GetDroneToList(id);
        var d = _bl.GetDrone(dl);
        return Ok(_map.ToDto(d));
    }

    [HttpPost]
    public IActionResult Create(CreateDroneRequest req)
    {
        var bo = new BO.Drone { Id = req.Id, Model = req.Model, MaxWeight = Enum.Parse<BO.WeightCategories>(req.Weight.ToString()) };
        _bl.AddDrone(bo, req.BaseStationId);
        return CreatedAtAction(nameof(GetOne), new { id = req.Id }, new { id = req.Id });
    }

    [HttpPatch("{id:int}/model")]
    public IActionResult UpdateModel(int id, UpdateModelRequest req)
    {
        _bl.UpdateModelDrone(id, req.Model);
        return NoContent();
    }

    [HttpPost("{id:int}/charge")]
    public IActionResult SendToCharge(int id) { _bl.SendDroneToCharge(id); return NoContent(); }

    [HttpPost("{id:int}/release")]
    public IActionResult ReleaseFromCharge(int id) { _bl.DroneFromCharge(id); return NoContent(); }

    [HttpPost("{id:int}/assign")]
    public IActionResult AssignParcel(int id) { _bl.ConnectParcelToDrone(id); return NoContent(); }

    [HttpPost("{id:int}/pickup")]
    public IActionResult Pickup(int id) { _bl.PickedUpParcelByDrone(id); return NoContent(); }

    [HttpPost("{id:int}/deliver")]
    public IActionResult Deliver(int id) { _bl.DeliveredParcelByDrone(id); return NoContent(); }
}
