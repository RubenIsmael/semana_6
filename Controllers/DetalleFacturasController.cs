using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Semana5.Data;
using Semana5.Models;

namespace Semana5.Controllers
{
    public class DetalleFacturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetalleFacturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetalleFacturas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DetalleFacturas.Include(d => d.Factura).Include(d => d.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DetalleFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetalleFacturas
                .Include(d => d.Factura)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DetalleFacturaId == id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            return View(detalleFactura);
        }

        // GET: DetalleFacturas/Create
        public IActionResult Create()
        {
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            return View();
        }

        // POST: DetalleFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturaId,ProductoId,Cantidad,PrecioUnitario")] DetalleFactura detalleFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", detalleFactura.FacturaId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", detalleFactura.ProductoId);
            return View(detalleFactura);
        }

        // Método para buscar productos
        public JsonResult SearchProducts(string term)
        {
            var products = _context.Productos
                .Where(p => p.Nombre.Contains(term))
                .Select(p => new { p.ProductoId, p.Nombre })
                .ToList();

            return Json(products);
        }

        // GET: DetalleFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetalleFacturas.FindAsync(id);
            if (detalleFactura == null)
            {
                return NotFound();
            }
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", detalleFactura.FacturaId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", detalleFactura.ProductoId);
            return View(detalleFactura);
        }

        // POST: DetalleFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleFacturaId,FacturaId,ProductoId,Cantidad,PrecioUnitario")] DetalleFactura detalleFactura)
        {
            if (id != detalleFactura.DetalleFacturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleFacturaExists(detalleFactura.DetalleFacturaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", detalleFactura.FacturaId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", detalleFactura.ProductoId);
            return View(detalleFactura);
        }

        // GET: DetalleFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetalleFacturas
                .Include(d => d.Factura)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DetalleFacturaId == id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            return View(detalleFactura);
        }

        // POST: DetalleFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleFactura = await _context.DetalleFacturas.FindAsync(id);
            if (detalleFactura != null)
            {
                _context.DetalleFacturas.Remove(detalleFactura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleFacturaExists(int id)
        {
            return _context.DetalleFacturas.Any(e => e.DetalleFacturaId == id);
        }
    }
}
