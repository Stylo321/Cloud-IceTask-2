using Kias_Kar_Kompany.Data;
using Kias_Kar_Kompany.Models;
using Microsoft.AspNetCore.Mvc;

public class ManufacturersController : Controller
{
    private readonly Kias_Kar_KompanyContext _context;

    public ManufacturersController(Kias_Kar_KompanyContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Manufacturer.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var manufacturer = await _context.Manufacturer
            .FirstOrDefaultAsync(m => m.Id == id);

        if (manufacturer == null) return NotFound();

        return View(manufacturer);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Manufacturer manufacturer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(manufacturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(manufacturer);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var manufacturer = await _context.Manufacturer.FindAsync(id);
        if (manufacturer == null) return NotFound();

        return View(manufacturer);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Manufacturer manufacturer)
    {
        if (id != manufacturer.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(manufacturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(manufacturer);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var manufacturer = await _context.Manufacturer.FindAsync(id);
        if (manufacturer == null) return NotFound();

        return View(manufacturer);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var manufacturer = await _context.Manufacturer.FindAsync(id);
        _context.Manufacturer.Remove(manufacturer);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
