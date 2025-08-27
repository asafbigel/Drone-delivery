namespace BlWebApi.Models;

public  class CustomerToListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
}

public  class CustomerDetailsDto : CustomerToListDto
{
    public string Address { get; set; } = "";
    public double? Lat { get; set; }
    public double? Lng { get; set; }
}

public  class UpdateCustomerRequest
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
