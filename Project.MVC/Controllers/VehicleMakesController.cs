using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Project.MVC.Models;
using Project.Service.DataAccess;
using Project.Service.Models;

using System.Threading.Tasks;

namespace Project.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IVehicleService<VehicleMake> _make;
        private readonly PageModel<VehicleMake> _page;
        private readonly FilterModel _filter;
        private readonly SortModel _sort;
        private readonly IMapper _mapper;

        public VehicleMakesController(IVehicleService<VehicleMake> make, PageModel<VehicleMake> page, FilterModel filter, SortModel sort, IMapper mapper)
        {
            _make = make;
            _page = page;
            _filter = filter;
            _sort = sort;
            _mapper = mapper;
        }

        // GET: VehicleMakes
        public async Task<IActionResult> Index(string filter, [FromQuery(Name = "pageNo")] int? page, string sortBy)
        {
            int pageNumber = page ?? 1;

            if (filter != null)
            {
                pageNumber = 1;
            }

            ViewBag.SortParam = sortBy;
            ViewBag.Filter = filter;

            _page.CurrentPageIndex = pageNumber;
            _sort.SortBy = sortBy;
            _filter.FilterString = filter;

            var dto = _mapper.Map<PageModelDTO<VehicleMakeDTO>>(await _make.FindAsync(_filter, _page, _sort));
            return View(dto);
        }

        // GET: VehicleMakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<VehicleMakeDTO>(await _make.GetAsync(id));
            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMakeDTO vehicleMake)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<VehicleMake>(vehicleMake);
                await _make.CreateAsync(dto);
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
            var dto = _mapper.Map<VehicleMakeDTO>(await _make.GetAsync(id));

            if (dto == null)
            {
                return NotFound();
            }
            return View(dto);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMakeDTO vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<VehicleMake>(vehicleMake);
                await _make.UpdateAsync(dto);
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

            var dto = _mapper.Map<VehicleMakeDTO>(await _make.GetAsync(id));
            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
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
