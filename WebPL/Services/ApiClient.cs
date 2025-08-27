
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WebPL.Models;

namespace WebPL.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _http;
        private static readonly JsonSerializerOptions _json = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiClient(HttpClient http) => _http = http;

        public async Task<List<DroneToListDto>> GetDrones(DroneStatus? status, WeightCategory? weight, CancellationToken ct = default)
        {
            var qs = new List<string>();
            if (status.HasValue) qs.Add($"status={(int)status.Value}");
            if (weight.HasValue) qs.Add($"weight={(int)weight.Value}");
            var url = "/api/Drones" + (qs.Count > 0 ? "?" + string.Join("&", qs) : "");
            var res = await _http.GetAsync(url, ct);
            res.EnsureSuccessStatusCode();
            var list = await res.Content.ReadFromJsonAsync<List<DroneToListDto>>(_json, ct);
            return list ?? new List<DroneToListDto>();
        }

        public async Task<DroneDetailsDto?> GetDrone(int id, CancellationToken ct = default)
        {
            var res = await _http.GetAsync($"/api/Drones/{id}", ct);
            if (!res.IsSuccessStatusCode) return null;
            return await res.Content.ReadFromJsonAsync<DroneDetailsDto>(_json, ct);
        }

        public async Task CreateDrone(CreateDroneRequest req, CancellationToken ct = default)
        {
            var res = await _http.PostAsJsonAsync("/api/Drones", req, _json, ct);
            res.EnsureSuccessStatusCode();
        }

        public async Task UpdateDroneModel(int id, string model, CancellationToken ct = default)
        {
            var body = new UpdateModelRequest { Model = model };
            var res = await _http.PatchAsJsonAsync($"/api/Drones/{id}/model", body, _json, ct);
            res.EnsureSuccessStatusCode();
        }

        public async Task DroneCommand(int id, string command, CancellationToken ct = default)
        {
            var res = await _http.PostAsync($"/api/Drones/{id}/{command}", null, ct);
            res.EnsureSuccessStatusCode();
        }
    }
}
