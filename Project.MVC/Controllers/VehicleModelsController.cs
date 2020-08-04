using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Project.Service.DataAccess;
using Project.Service.Models;

using System.Threading.Tasks;

namespace Project.MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly IVehicleService<VehicleModel> _model;
        private readonly IVehicleService<VehicleMake> _make;

        public VehicleModelsController(IVehicleService<VehicleModel> model, IVehicleService<VehicleMake> make)
        {
            _model = model;
            _make = make;
        }

        // GET: VehicleModels
        public async Task<IActionResult> Index()
        {
            var result = (await _model.FindAsync("", "Id")).Results.Include(v => v.SelectedVehicleMake);
            return View(await result.ToListAsync());
        }

        // GET: VehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await (await _model.FindAsync("", "Id")).Results
                .Include(v => v.SelectedVehicleMake)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public async Task<IActionResult> Create()
        {
            var result = await (await _make.FindAsync("", "Id")).Results.ToListAsync();
            ViewData["MakeId"] = new SelectList(result, "Id", "Name");
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
                await _model.CreateAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            var result = await (await _make.FindAsync("", "Id")).Results.ToListAsync();
            ViewData["MakeId"] = new SelectList(result, "Id", "Name");
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _model.GetAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            var result = await (await _make.FindAsync("", "Id")).Results.ToListAsync();
            ViewData["MakeId"] = new SelectList(result, "Id", "Name");
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
                await _model.UpdateAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            var result = await (await _model.FindAsync("", "Id")).Results.ToListAsync();
            ViewData["MakeId"] = new SelectList(result, "Id", "Name");
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await (await _model.FindAsync("", "Id")).Results
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
            await _model.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
