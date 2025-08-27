
using WebPL.Models;

namespace WebPL.Services
{
    public interface IApiClient
    {
        Task<List<DroneToListDto>> GetDrones(DroneStatus? status, WeightCategory? weight, CancellationToken ct = default);
        Task<DroneDetailsDto?> GetDrone(int id, CancellationToken ct = default);
        Task CreateDrone(CreateDroneRequest req, CancellationToken ct = default);
        Task UpdateDroneModel(int id, string model, CancellationToken ct = default);
        Task DroneCommand(int id, string command, CancellationToken ct = default); // charge/release/assign/pickup/deliver
    }
}
