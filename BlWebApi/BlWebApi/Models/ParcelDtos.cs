namespace BlWebApi.Models;

public enum ParcelStatus { Created, Scheduled, PickedUp, Delivered }

public  class ParcelToListDto
{
    public int Id { get; set; }
    public int? DroneId { get; set; }
    public string Sender { get; set; } = "";
    public string Receiver { get; set; } = "";
    public ParcelStatus Status { get; set; }
    public WeightCategory Weight { get; set; }
}

public  class ParcelDetailsDto : ParcelToListDto
{
    public DateTime? Scheduled { get; set; }
    public DateTime? PickedUp { get; set; }
    public DateTime? Delivered { get; set; }
}

public  class CreateParcelRequest
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public WeightCategory Weight { get; set; }
}
