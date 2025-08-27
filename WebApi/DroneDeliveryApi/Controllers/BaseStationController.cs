using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BlApi;
using BO;

namespace DroneDeliveryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseStationController : ControllerBase
    {
        private readonly IBL _bl;

        public BaseStationController(IBL bl)
        {
            _bl = bl;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BaseStationToList>> GetBaseStations()
        {
            try
            {
                return Ok(_bl.GetAllBaseStations(_ => true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddBaseStation([FromBody] BaseStation baseStation)
        {
            try
            {
                _bl.AddBaseStation(baseStation);
                return StatusCode(201, "Base station added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding base station: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBaseStation(int id, [FromBody] BaseStation baseStation)
        {
            try
            {
                if (id != baseStation.Id)
                {
                    return BadRequest("Base station ID in URL does not match ID in body.");
                }
                _bl.UpdateBaseStation(baseStation);
                return Ok("Base station updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating base station: {ex.Message}");
            }
        }
    }
}