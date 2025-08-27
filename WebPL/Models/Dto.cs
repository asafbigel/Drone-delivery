
using System.Text.Json.Serialization;

namespace WebPL.Models
{
    public enum DroneStatus { Available = 0, Maintenance = 1, Delivery = 2 }
    public enum WeightCategory { Light = 0, Medium = 1, Heavy = 2 }
    public enum ParcelStatus { Created = 0, Scheduled = 1, PickedUp = 2, Delivered = 3 }

    public class DroneToListDto
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public DroneStatus Status { get; set; }
        public WeightCategory Weight { get; set; }
        public int Battery { get; set; }
        public string? CurrentLocation { get; set; }
        public int? CurrentParcelId { get; set; }
    }

    public class DroneDetailsDto : DroneToListDto
    {
        public string? Notes { get; set; }
    }

    public class CreateDroneRequest
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public WeightCategory Weight { get; set; }
        public int BaseStationId { get; set; }
    }

    public class UpdateModelRequest
    {
        public string? Model { get; set; }
    }

    public class CustomerToListDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
    }

    public class CustomerDetailsDto : CustomerToListDto
    {
        public string? Address { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }

    public class BaseStationToListDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ChargingSlots { get; set; }
        public int OccupiedSlots { get; set; }
    }

    public class BaseStationDetailsDto : BaseStationToListDto
    {
        public string? Location { get; set; }
    }

    public class ParcelToListDto
    {
        public int Id { get; set; }
        public int? DroneId { get; set; }
        public string? Sender { get; set; }
        public string? Receiver { get; set; }
        public ParcelStatus Status { get; set; }
        public WeightCategory Weight { get; set; }
    }

    public class ParcelDetailsDto : ParcelToListDto
    {
        public DateTimeOffset? Scheduled { get; set; }
        public DateTimeOffset? PickedUp { get; set; }
        public DateTimeOffset? Delivered { get; set; }
    }

    public class CreateParcelRequest
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public WeightCategory Weight { get; set; }
    }
}
