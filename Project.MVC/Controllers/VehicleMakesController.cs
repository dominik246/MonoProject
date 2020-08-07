using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Project.Service.DataAccess;
using Project.Service.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IVehicleService<VehicleMake> _make;
        private const int pageSize = 5;

        public VehicleMakesController(IVehicleService<VehicleMake> make)
        {
            _make = make;
        }

        // GET: VehicleMakes
        public async Task<IActionResult> Index([FromQuery(Name = "sortBy")] string sortBy, string currentFilter, string filter, [FromQuery(Name = "pageNo")] int? page)
        {
            int pageNumber = page ?? 1;

            if (filter != null)
            {
                pageNumber = 1;
            }
            else
            {
                filter = currentFilter;
            }

            ViewBag.CurrentFilter = filter;

            ViewBag.NameSortParam = sortBy == "Name_desc" ? "Name" : "Name_desc";
            ViewBag.AbrvSortParam = sortBy == "Abrv_desc" ? "Abrv" : "Abrv_desc";

            List<VehicleMake> list;
            PagedResult<VehicleMake> pagedResult;

            switch (sortBy)
            {
                case "Abrv":
                case "Abrv_desc":
                    pagedResult = await _make.FindAsync(filter, ViewBag.AbrvSortParam as string, pageNumber, pageSize);
                    list = await pagedResult.Results.ToListAsync();
                    break;
                case "Name":
                case "Name_desc":
                default:
                    pagedResult = await _make.FindAsync(filter, ViewBag.MakeSortParam as string, pageNumber, pageSize);
                    list = await pagedResult.Results.ToListAsync();
                    break;
            }
            pagedResult.ListResults = list;
            return View(pagedResult);
        }

        // GET: VehicleMakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _make.GetAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // GET: VehicleMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                await _make.CreateAsync(vehicleMake);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _make.GetAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _make.UpdateAsync(vehicleMake);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _make.GetAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _make.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
