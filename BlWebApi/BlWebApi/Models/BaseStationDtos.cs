namespace BlWebApi.Models;

public  class BaseStationToListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int ChargingSlots { get; set; }
    public int OccupiedSlots { get; set; }
}

public  class BaseStationDetailsDto : BaseStationToListDto
{
    public string Location { get; set; } = "";
}

public  class UpdateBaseStationRequest
{
    public string? Name { get; set; }
    public int? ChargingSlots { get; set; }
}
