namespace SkyTravel.Models;

public class Status
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Fly> Flights { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}