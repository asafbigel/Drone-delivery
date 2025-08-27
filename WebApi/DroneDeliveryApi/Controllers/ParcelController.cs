using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BlApi;
using BO;

namespace DroneDeliveryApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ParcelController : ControllerBase
	{
		private readonly IBL _bl;

		public ParcelController(IBL bl)
		{
			_bl = bl;
		}

		[HttpGet]
		public ActionResult<IEnumerable<ParcelToList>> GetParcels()
		{
			try
			{
				return Ok(_bl.GetAllParcels(_ => true));
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPost]
		public IActionResult AddParcel([FromBody] Parcel parcel)
		{
			try
			{
				_bl.AddParcel(parcel);
				return StatusCode(201, "Parcel added successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error adding parcel: {ex.Message}");
			}
		}

		[HttpPut("{id}")]
		public IActionResult UpdateParcel(int id, [FromBody] Parcel parcel)
		{
			try
			{
				if (id != parcel.Id)
				{
					return BadRequest("Parcel ID in URL does not match ID in body.");
				}
				_bl.UpdateParcel(parcel);
				return Ok("Parcel updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error updating parcel: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteParcel(int id)
		{
			try
			{
				_bl.DeleteParcel(id);
				return Ok("Parcel deleted successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error deleting parcel: {ex.Message}");
			}
		}
	}
}
