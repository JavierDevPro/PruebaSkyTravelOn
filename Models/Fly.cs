using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyTravel.Models;

public class Fly
{
    public int Id { get; set; }
    
    public string Code { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime TimeOfExit { get; set; }
    public DateTime TimeOfArrival { get; set; }
    public int TotalSeats { get; set; }
    
    public int StatusId { get; set; }
    public Status Status { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}