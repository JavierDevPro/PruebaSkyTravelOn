using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyTravel.Data;
using SkyTravel.Models;

namespace SkyTravel.Controllers;

public class PassengerController : Controller
{
    private readonly AppDbContext _context;

    public PassengerController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Passenger> passengers = await _context.Passengers.ToListAsync();
        return View(passengers);
    }
    
    [HttpGet]
    public async Task<IActionResult> ListAll()
    {
        IEnumerable<Passenger> passengers = await _context.Passengers.ToListAsync();
        return RedirectToAction("Index", passengers);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(Passenger entity)
    {
        try
        {
            if (await Exist(entity.Document))
            {
                //dato temporal con alerta
                TempData["Error"] = $"El documento {entity.Document} ya está registrado";
                return RedirectToAction(nameof(ListAll));
            }

            _context.Passengers.Add(entity);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Usuario registrado correctamente";
            return RedirectToAction(nameof(ListAll));
        }
        catch (Exception)
        {
            return null;
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var passenger = _context.Passengers.FirstOrDefault(p => p.Id == id);
        return View(passenger);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Passenger UpdateData)
    {
        try
        {
            var passenger = await findOn(id);

            if (passenger != null)
            {
                passenger.Document = UpdateData.Document;
                passenger.Email = UpdateData.Email;
                passenger.FullName = UpdateData.FullName;
                passenger.PhoneNumber = UpdateData.PhoneNumber;

                _context.Update(passenger);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Pasajero actualizado exitosamente!";
            }
            else
            {
                TempData["Error"] = $"El pasajero con el documento: {UpdateData.Document} no se pudo encontrar";
                return View("Index");
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["Error"] = $"ERROR: {e.Message}";
            return RedirectToAction("Index");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int Id)
    {
        try
        {
            var passenger = await findOn(Id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Pasajero actualizado exitosamente!";
            }else
            {
                TempData["Error"] = $"El pasajero no se pudo encontrar";
            }
            return RedirectToAction("Index");
        }
        catch (HttpRequestException e)
        {
            TempData["Error"] = $"Error al intentar eliminar en la Base da datos: {e.Message}";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            TempData["Error"] = $"ERROR: {e.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<Passenger> findOn(int id)
    {
        try
        {
            var passenger = await _context.Passengers.FirstOrDefaultAsync(p => p.Id == id);
            return passenger;
        }
        catch (HttpRequestException e)
        {
            return null;
        }
    }
    public async Task<bool> Exist(string document)
    {
        var passenger  = await _context
            .Passengers
            .Where(p => p.Document == document)
            .ToListAsync();
        if (passenger.Any())
        {
            return true;
        }
        return false;
    }
}