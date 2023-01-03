using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageMVC.Data;
using GarageMVC.Models.ViewModels;
using GarageMVC.Models.Entities;

namespace GarageMVC.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly GarageMVCContext _context;

        public VehiclesController(GarageMVCContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Fullview()
        {
            return _context.Vehicle != null ?
                        View(await _context.Vehicle.ToListAsync()) :
                        Problem("Entity set 'GarageMVCContext.Vehicle'  is null.");

        }
        // GET: Vehicles Overview
        public async Task<IActionResult> Index()
        {
           
            IEnumerable<VehicleOverview> overview =
                await _context.Vehicle.Select(v => new VehicleOverview()
                {
                    Id = v.Id,
                    VehicleType = v.VehicleType,
                    RegNo = v.RegNo,
                    TimeOfArrival = v.TimeOfArrival
                })
                .OrderBy(p => p.RegNo)
                .ToListAsync();

            if (TempData.ContainsKey("parked"))
            {
                ViewData["ShowElement"] = TempData["parked"];
            }
            if (TempData.ContainsKey("checkedout"))
            {
                ViewData["ShowElement"] = TempData["checkedout"];
            }
            if (TempData.ContainsKey("updated"))
            {
                ViewData["ShowElement"] = TempData["updated"];
            }
            if (TempData.ContainsKey("RegNo"))
            {
                ViewData["RegNo"] = TempData["RegNo"];
            }
            if (TempData.ContainsKey("VehicleType"))
            {
                ViewData["VehicleType"] = TempData["VehicleType"];
            }
           

            return _context.Vehicle != null ?
                        View(overview) :
                        Problem("Entity set 'GarageMVCContext.Vehicle'  is null.");
        }

        // Testing - Search
        public async Task<IActionResult> Search(string searchString)
        {
            IEnumerable<VehicleOverview> overview =
                await _context.Vehicle
                .Where(v => v.RegNo.StartsWith(searchString))
                .Select(v => new VehicleOverview
                {
                    Id = v.Id,
                    VehicleType = v.VehicleType,
                    RegNo = v.RegNo,
                    TimeOfArrival = v.TimeOfArrival
                })
                .ToListAsync();
            return _context.Vehicle != null ?
                View("Index", overview) :
                Problem("Entity set 'GarageMVCContext.Vehicle'  is null.");
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleType,RegNo,Color,Brand,Model,NoOfWheels")] Vehicle vehicle)
        {
            
            if (ModelState.IsValid)
            {
                var vehicleExists = await _context.Vehicle.FirstOrDefaultAsync(v => v.RegNo == vehicle.RegNo);

                if (vehicleExists != null)
                {
                    ViewData["VehicleType"] = vehicleExists.VehicleType;
                    ViewData["RegNo"] = vehicleExists.RegNo;
                    TempData["error"] = "error";
                    return View(vehicle);
                }

                _context.Add(vehicle);
                await _context.SaveChangesAsync();

                TempData["parked"] = "parked";
                TempData["RegNo"] = vehicle.RegNo;
                TempData["VehicleType"] = Enum.GetName(typeof(VehicleType), vehicle.VehicleType);
                ViewData["ModelState"] = ModelState.Values;

                return RedirectToAction(nameof(Index));
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,RegNo,Color,Brand,Model,NoOfWheels")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {               
                var  vehicleToUpdate = await _context.Vehicle.FirstOrDefaultAsync(v => v.Id == id);

                if (await TryUpdateModelAsync<Vehicle>(vehicleToUpdate!, "",
                    v => v.VehicleType,
                    v => v.RegNo,
                    v => v.Color,
                    v => v.Brand,
                    v => v.Model,
                    v => v.NoOfWheels))

                    try
                    {

                        // _context.Update(vehicle);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!VehicleExists(vehicle.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                TempData["updated"] = "updated";
                TempData["RegNo"] = vehicle.RegNo;
                TempData["VehicleType"] = Enum.GetName(typeof(VehicleType), vehicle.VehicleType);
                TempData["ModelState"] = ModelState;

                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'GarageMVCContext.Vehicle'  is null.");
            }
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                TempData["checkedout"] = "checkedout";
                TempData["RegNo"] = vehicle.RegNo;
                TempData["VehicleType"] = Enum.GetName(typeof(VehicleType), vehicle.VehicleType);

                _context.Vehicle.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Vehicles/Receipt
        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return (_context.Vehicle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
