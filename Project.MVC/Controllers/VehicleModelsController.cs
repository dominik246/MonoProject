using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Service.DataAccess;
using Project.Service.Models;

namespace Project.MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly IVehicleService<VehicleModel> _service;

        public VehicleModelsController(IVehicleService<VehicleModel> service)
        {
            _service = service;
        }

        // GET: VehicleModels
        public async Task<IActionResult> Index()
        {
            var serviceDBContext = (await _service.FindAsync("", "Id")).Results.Include(v => v.SelectedVehicleMake);
            return View(await serviceDBContext.ToListAsync());
        }

        // GET: VehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _service.GetAsync(id);

            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public async Task<IActionResult> Create()
        {
            var result = await (await _service.FindAsync("", "Id")).Results.Include(v => v.SelectedVehicleMake).ToListAsync();
            ViewData["MakeId"] = new SelectList(result.ConvertAll(s => s.SelectedVehicleMake), "Id", "Name");
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            var result = await (await _service.FindAsync("", "Id")).Results.ToListAsync();
            ViewData["MakeId"] = new SelectList(result, "Id", "Abrv", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _service.GetAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            var result = await (await _service.FindAsync("", "Id")).Results.ToListAsync();
            ViewData["MakeId"] = new SelectList(result, "Id", "Abrv", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            var result = await (await _service.FindAsync("", "Id")).Results.ToListAsync();
            ViewData["MakeId"] = new SelectList(result, "Id", "Abrv", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await (await _service.FindAsync("", "Id")).Results
                .Include(v => v.SelectedVehicleMake)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
