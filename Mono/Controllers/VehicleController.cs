using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.Data;
using Mono.Interfaces;
using Mono.Models;

namespace Mono.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IVehicles _service;

        public VehicleController(ApplicationDbContext db, IVehicles service)
        {
            _db = db;
            _service = service;
        }

        public IActionResult Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3)
        {
         
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name" : "";

            var vehicles = from b in _db.VehicleMake
                           select b;

            var vehicleCount = vehicles.Count();

            //FILTERING
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(b => b.name.ToLower().Contains(searchString));
                vehicleCount = vehicles.Count();
            }

            //SORTING
            switch (sortOrder)
            {
                case "name":
                    vehicles = vehicles.OrderByDescending(b => b.name);
                    break;
                default:
                    vehicles = vehicles.OrderBy(b => b.name);
                    break;
            }

            //PAGING
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            vehicles = vehicles
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<VehicleMake>
            {
                Data = vehicles.AsNoTracking().ToList(),
                TotalItems = vehicleCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        //CREATE
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleMake obj)
        {

            if (_service.AddMake(ModelState, obj))
            {
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //EDIT
        //GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            if (_service.UpdateDeleteMakeGet(id) == null)
            {
                return NotFound();
            }

            return View(await _service.UpdateDeleteMakeGet(id));
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleMake obj)
        {

            if (_service.UpdatePostMake(ModelState, obj))
            {
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //DELETE
        //GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            if (_service.UpdateDeleteMakeGet(id) == null)
            {
                return NotFound();
            }

            return View(await _service.UpdateDeleteMakeGet(id));
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            Task<bool> deleteTask = _service.DeletePostMake(id);

            bool b = await deleteTask;

            if (b)
            {
                return RedirectToAction("Index");
            }

            return View();       
        }
    }
}
