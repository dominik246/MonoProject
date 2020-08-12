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
        private readonly PageModel<VehicleModel> _pageModel;
        private readonly FilterModel _filter;
        private readonly SortModel _sort;

        public VehicleModelsController(IVehicleService<VehicleModel> model, IVehicleService<VehicleMake> make, PageModel<VehicleModel> page, FilterModel filter, SortModel sort)
        {
            _model = model;
            _make = make;
            _pageModel = page;
            _filter = filter;
            _sort = sort;
        }

        // GET: VehicleModels
        public async Task<IActionResult> Index([FromQuery(Name = "sortBy")] string sortBy, [FromQuery(Name = "pageNo")] int? page, string currentFilter, string filter = "")
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

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Contains("_desc"))
                {
                    sortBy = sortBy.Replace("_desc", "");
                }
                else
                {
                    sortBy += "_desc";
                }
            }

            _pageModel.CurrentPageIndex = pageNumber;
            _sort.SortBy = sortBy;
            _filter.FilterString = filter;

            ViewBag.CurrentFilter = filter;

            ViewBag.MakeSortParam = sortBy == "Make" ? "Make_desc" : "Make";
            ViewBag.NameSortParam = sortBy == "Name" ? "Name_desc" : "Name";
            ViewBag.AbrvSortParam = sortBy == "Abrv" ? "Abrv_desc" : "Abrv";

            PageModel<VehicleModel> result = await _model.FindAsync(_filter, _pageModel, _sort);
            return View(result);
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
            var result = await _make.FindAsync(_filter, null, _sort);
            var list = result.ListResult;
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
            var result = await _make.FindAsync(_filter, null, _sort);
            var list = result.ListResult;
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
            var result = await _make.FindAsync(_filter, null, _sort);
            var list = result.ListResult;
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
            var result = await _make.FindAsync(_filter, null, _sort);
            var list = result.ListResult;
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
