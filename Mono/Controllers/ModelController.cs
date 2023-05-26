﻿using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.Data;
using Mono.Interfaces;
using Mono.Models;

namespace Mono.Controllers
{
    public class ModelController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IVehicles _service;

        public ModelController(ApplicationDbContext db, IVehicles service)
        {
            _db = db;
            _service = service;
        }

        public IActionResult Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name" : "";

            var vehicles = from b in _db.VehicleModel
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

            var result = new PagedResult<VehicleModel>
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
        public IActionResult Create(VehicleModel obj)
        {

            if (_service.AddModel(ModelState, obj))
            {
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //EDIT
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            if (_service.UpdateDeleteModelGet(id) == null)
            {
                return NotFound();
            }

            return View(_service.UpdateDeleteModelGet(id));
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleModel obj)
        {

            if (_service.UpdatePostModel(ModelState, obj))
            {
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //DELETE
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            if (_service.UpdateDeleteModelGet(id) == null)
            {
                return NotFound();
            }

            return View(_service.UpdateDeleteModelGet(id));
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            if (_service.DeletePostModel(id))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
