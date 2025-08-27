
using Microsoft.AspNetCore.Mvc;
using WebPL.Models;
using WebPL.Services;

namespace WebPL.Controllers
{
    public class DronesController : Controller
    {
        private readonly IApiClient _api;
        public DronesController(IApiClient api) => _api = api;

        public async Task<IActionResult> Index(DroneStatus? status, WeightCategory? weight, CancellationToken ct)
        {
            var drones = await _api.GetDrones(status, weight, ct);
            ViewBag.Status = status;
            ViewBag.Weight = weight;
            return View(drones);
        }

        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var drone = await _api.GetDrone(id, ct);
            if (drone == null) return NotFound();
            return View(drone);
        }

        [HttpGet]
        public IActionResult Create() => View(new CreateDroneRequest());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDroneRequest model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(model);
            await _api.CreateDrone(model, ct);
            TempData["msg"] = $"Drone {model.Id} created";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateModel(int id, string model, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                TempData["err"] = "Model cannot be empty";
                return RedirectToAction(nameof(Details), new { id });
            }
            await _api.UpdateDroneModel(id, model, ct);
            TempData["msg"] = "Model updated";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Command(int id, string actionName, CancellationToken ct)
        {
            // actionName: charge | release | assign | pickup | deliver
            await _api.DroneCommand(id, actionName, ct);
            TempData["msg"] = $"Command '{actionName}' sent";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
