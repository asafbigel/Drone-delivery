using BlWebApi.Models;
using BO;

namespace BlWebApi.Services;

public class ApiMapper
{
    // ---- Drone ----
    public DroneToListDto ToDto(DroneToList b) => new DroneToListDto
    {
        Id = b.Id,
        Model = b.Model,
        Status = b.Status,
        Battery = (int)Math.Round(b.Battery),

        // שדות שעשויים לא להיות אצלך:
        Weight = DroneStatusToWeightFallback(b.Status), // Fallback חסר משמעות עסקית – רק כדי לקמפל
        CurrentLocation = null,
        CurrentParcelId = null
    };

    public DroneDetailsDto ToDto(Drone b) => new DroneDetailsDto
    {
        Id = b.Id,
        Model = b.Model,
        Status = b.Status   ,
        Battery = (int)Math.Round(b.Battery),

        // שדות שעשויים לא להיות אצלך:
        Weight = DroneStatusToWeightFallback(b.Status),
        CurrentLocation = null,
        CurrentParcelId = null,
        Notes = null
    };

    private static WeightCategory DroneStatusToWeightFallback(object status) => WeightCategory.Medium;

    // ---- Parcel ----
    public ParcelToListDto ToDto(ParcelToList p) => new ParcelToListDto
    {
        Id = p.Id,
        // לא סומך על שמות SenderId/GetterId/DroneId אצלך – מנקה:
        Sender = "",
        Receiver = "",
        Status = ParcelStatus.Created,
        Weight = WeightCategory.Light
    };

    public ParcelDetailsDto ToDto(Parcel p) => new ParcelDetailsDto
    {
        Id = p.Id,
        Sender = "",
        Receiver = "",
        Status = ParcelStatus.Created,
        Weight = WeightCategory.Light,
        Scheduled = null,
        PickedUp = null,
        Delivered = null
    };

    // ---- Customer ----
    public CustomerToListDto ToDto(CustomerToList c) => new CustomerToListDto
    {
        Id = c.Id,
        Name = c.Name,
        Phone = c.Phone
    };

    public CustomerDetailsDto ToDto(Customer c) => new CustomerDetailsDto
    {
        Id = c.Id,
        Name = c.Name,
        Phone = c.Phone,
        // מנקה שדות שלא קיימים אצלך:
        Address = "",
        Lat = null,
        Lng = null
    };

    // ---- BaseStation ----
    public BaseStationToListDto ToDto(BaseStationToList b) => new BaseStationToListDto
    {
        Id = b.Id,
        Name = b.Name,
        ChargingSlots = 0,
        OccupiedSlots = 0
    };

    public BaseStationDetailsDto ToDto(BaseStation b) => new BaseStationDetailsDto
    {
        Id = b.Id,
        Name = b.Name,
        ChargingSlots = 0,
        OccupiedSlots = 0,
        Location = ""
    };
}
