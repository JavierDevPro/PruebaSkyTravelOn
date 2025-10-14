using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyTravel.Data;
using SkyTravel.Models;

namespace SkyTravel.Controllers;

public class FlyController : Controller
{
    private readonly AppDbContext _context;

    public FlyController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Fly> flights = await _context.Flights.ToListAsync();
        return View(flights);
    }

    public async Task<IActionResult> Register(Fly fly)
    {
        try
        {
            if (_context.Flights.Any(f => f.Code == fly.Code))
            {
                TempData["AddError"] = $"ERROR: El codigo {fly.Code} de vuelo ya existe.";
                return RedirectToAction(nameof(Index));
            }

            _context.Flights.Add(fly);
            await _context.SaveChangesAsync();
            TempData["AddSuccess"] = "Vuelo registrado correctamente.";
        }
        catch (HttpRequestException e)
        {
            TempData["AddError"] = $"ERROR: No se pudo realizar la peticion a la DB: \n{e.Message}";
        }
        catch (Exception e)
        {
            TempData["AddError"] = $"ERROR: Problema por: \n{e.Message}";
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Fly fly)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return View(fly);
    }
}