using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Project.Service.DataAccess;
using Project.Service.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly IVehicleService<VehicleModel> _model;
        private readonly IVehicleService<VehicleMake> _make;
        private const int pageSize = 10;

        public VehicleModelsController(IVehicleService<VehicleModel> model, IVehicleService<VehicleMake> make)
        {
            _model = model;
            _make = make;
        }

        // GET: VehicleModels
        public async Task<IActionResult> Index([FromQuery(Name = "sortBy")] string sortBy, [FromQuery(Name = "pageNo")] int? page, string currentFilter, string filter = "")
        {
            int pageNumber = page ?? 1;

            if (filter != null)
            {
                page = 1;
            }
            else
            {
                filter = currentFilter;
            }

            ViewBag.CurrentFilter = filter;

            ViewBag.MakeSortParam = sortBy == "Make_desc" ? "Make" : "Make_desc";
            ViewBag.NameSortParam = sortBy == "Name_desc" ? "Name" : "Name_desc";
            ViewBag.AbrvSortParam = sortBy == "Abrv_desc" ? "Abrv" : "Abrv_desc";

            List<VehicleModel> list;
            PagedResult<VehicleModel> pagedResult;



            switch (sortBy)
            {
                case "Name":
                case "Name_desc":
                    pagedResult = await _model.FindAsync(filter, ViewBag.NameSortParam as string, pageNumber, pageSize);
                    list = await pagedResult.Results.ToListAsync();
                    break;
                case "Abrv":
                case "Abrv_desc":
                    pagedResult = await _model.FindAsync(filter, ViewBag.AbrvSortParam as string, pageNumber, pageSize);
                    list = await pagedResult.Results.ToListAsync();
                    break;
                case "Make":
                case "Make_desc":
                default:
                    pagedResult = await _model.FindAsync(filter, ViewBag.MakeSortParam as string, pageNumber, pageSize);
                    list = await pagedResult.Results.ToListAsync();
                    list = await Task.FromResult((await Task.FromResult(ViewBag.MakeSortParam == "Make" ? list.OrderBy(m => m.SelectedVehicleMake.Name) : list.OrderByDescending(m => m.SelectedVehicleMake.Name))).ToList());
                    break;
            }
            pagedResult.ListResults = list;
            return View(pagedResult);
        }

        // GET: VehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public async Task<IActionResult> Create()
        {
            var result = await _make.FindAsync("", "Id", paged: false);
            var list = result.ListResults;
            ViewData["MakeId"] = new SelectList(list, "Id", "Name");
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
            var result = await _make.FindAsync("", "Id", paged: false);
            var list = result.ListResults;
            ViewData["MakeId"] = new SelectList(list, "Id", "Name");
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
            var result = await _make.FindAsync("", "Id", paged: false);
            var list = result.ListResults;
            ViewData["MakeId"] = new SelectList(list, "Id", "Name");
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
            var result = await _make.FindAsync("", "Id", paged: false);
            var list = result.ListResults;
            ViewData["MakeId"] = new SelectList(list, "Id", "Name");
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
