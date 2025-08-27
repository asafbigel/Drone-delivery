using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BlApi;
using BO;

namespace DroneDeliveryApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DroneController : ControllerBase
	{
		private readonly IBL _bl;

		public DroneController(IBL bl)
		{
			_bl = bl;
		}

		[HttpGet]
		public ActionResult<IEnumerable<DroneToList>> GetDrones()
		{
			try
			{
				return Ok(_bl.GetAllDrones(_ => true));
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPost]
		public IActionResult AddDrone([FromBody] Drone drone, [FromQuery] int baseStationNum)
		{
			try
			{
				_bl.AddDrone(drone, baseStationNum);
				return StatusCode(201, "Drone added successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error adding drone: {ex.Message}");
			}
		}

		[HttpPut("{id}/model")]
		public IActionResult UpdateDroneModel(int id, [FromBody] string model)
		{
			try
			{
				_bl.UpdateModelDrone(id, model);
				return Ok("Drone model updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error updating drone model: {ex.Message}");
			}
		}
	}
}
