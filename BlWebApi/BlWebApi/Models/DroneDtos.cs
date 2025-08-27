namespace BlWebApi.Models;

public enum DroneStatus { Vacant, Maintenance, InDelivery }
public enum WeightCategory { Light, Medium, Heavy }

public  class DroneToListDto
{
    public int Id { get; set; }
    public string Model { get; set; } = "";
    public BO.DroneStatuses Status { get; set; }
    public WeightCategory Weight { get; set; }
    public int Battery { get; set; }
    public string? CurrentLocation { get; set; }
    public int? CurrentParcelId { get; set; }
}

public  class DroneDetailsDto : DroneToListDto
{
    public string? Notes { get; set; }
}

public  class UpdateModelRequest
{
    public string Model { get; set; } = "";
}

public  class CreateDroneRequest
{
    public int Id { get; set; }
    public string Model { get; set; } = "";
    public WeightCategory Weight { get; set; }
    public int BaseStationId { get; set; }
}
