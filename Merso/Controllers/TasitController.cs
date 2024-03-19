using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Merso.Data;
using Merso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Merso.Controllers
{
    public class TasitController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TasitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasits.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasit = await _context.Tasits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasit == null)
            {
                return NotFound();
            }

            return View(tasit);
        }

        // GET: Cars/Create
       // public IActionResult Create()
        //{
        //    return View();
        //}




        public IActionResult Create()
        {
            // Veritabanından markaları al
            var markalar = _context.Brands.ToList();

            // Döküman menüsü için bir SelectList oluştur
            ViewBag.BrandSelectList = new SelectList(markalar, "Id", "Name");

            return View();
        }



        // POST: Cars/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarPlate,Km,Model,Color,GearType,Brand")] Tasit tasit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tasit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tasit);
        }

        // GET: Cars/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //  if (id == null)
        // {
        //    return NotFound();
        //}

        //var tasit = await _context.Tasits.FindAsync(id);
        //if (tasit == null)
        //{
        //   return NotFound();
        //}
        //return View(tasit);
        //}

        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve car data
            var car = await _context.Tasits.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            // Retrieve brands from the database (same logic as in Create)
            var brands = _context.Brands.ToList();
            ViewBag.BrandSelectList = new SelectList(brands, "Id", "Name"); // Cast brands to list

            return View(car);
        }

        // POST: Cars/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarPlate,Km,Model,Color,GearType,Brand")] Tasit tasit)
        {
            if (id != tasit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasitExists(tasit.Id))
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
            return View(tasit);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasit = await _context.Tasits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasit == null)
            {
                return NotFound();
            }

            return View(tasit);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasit = await _context.Tasits.FindAsync(id);
            if (tasit != null)
            {
                _context.Tasits.Remove(tasit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasitExists(int id)
        {
            return _context.Tasits.Any(e => e.Id == id);
        }
    }

}

