using Kias_Kar_Kompany.Data;
using Kias_Kar_Kompany.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class VehiclesController : Controller
{
    private readonly Kias_Kar_KompanyContext _context;

    public VehiclesController(Kias_Kar_KompanyContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Vehicle.Include(v => v.Manufacturer).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var vehicle = await _context.Vehicle
            .Include(v => v.Manufacturer)
            .FirstOrDefaultAsync(m => m.VehicleId == id);

        if (vehicle == null) return NotFound();

        return View(vehicle);
    }

    public IActionResult Create()
    {
        ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Manufacturer_Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Vehicle vehicle)
    {
        if (ModelState.IsValid)
        {
            _context.Add(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Manufacturer_Name", vehicle.ManufacturerId);
        return View(vehicle);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var vehicle = await _context.Vehicle.FindAsync(id);
        if (vehicle == null) return NotFound();

        ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Manufacturer_Name", vehicle.ManufacturerId);
        return View(vehicle);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Vehicle vehicle)
    {
        if (id != vehicle.VehicleId) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(vehicle);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var vehicle = await _context.Vehicle
            .Include(v => v.Manufacturer)
            .FirstOrDefaultAsync(m => m.VehicleId == id);

        if (vehicle == null) return NotFound();

        return View(vehicle);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var vehicle = await _context.Vehicle.FindAsync(id);
        _context.Vehicle.Remove(vehicle);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
