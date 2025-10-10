namespace SkyTravel.Models;

public class Reservation
{
    public int Id { get; set; }
    
    public string Code { get; set; }
    public int StatusId { get; set; }
    public int FlyId { get; set; }
    public int PassengerId { get; set; }
    
    public Status Status { get; set; }
    public Fly Fly { get; set; }
    public Passenger Passenger { get; set; }
}