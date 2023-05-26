using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.Data;
using Mono.Interfaces;
using System.Drawing.Printing;

namespace Mono.Models
{
    public class VehicleServices : IVehicles
    {
        private readonly ApplicationDbContext _db;

        public VehicleServices(ApplicationDbContext db)
        {
            _db = db;
        }

        // VEHICLEMAKE
        // ADD POST
        public bool AddMake(ModelStateDictionary ModelState, VehicleMake obj)
        {
            if (ModelState.IsValid)
            {
                 _db.VehicleMake.Add(obj);
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // UPDATE/DELETE GET
        public async Task<VehicleMake?> UpdateDeleteMakeGet(int? id)
        {  
            var vehicleFromDb = await _db.VehicleMake.FindAsync(id); 

            return vehicleFromDb;
        }

        // UPDATE POST
        public bool UpdatePostMake(ModelStateDictionary ModelState, VehicleMake obj)
        {
            if (ModelState.IsValid)
            {
                _db.VehicleMake.Update(obj);
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // DELETE POST
        public async Task<bool> DeletePostMake(int? id)
        {
            var obj = await _db.VehicleMake.FindAsync(id);

            if (obj != null)
            {
                _db.VehicleMake.Remove(obj);
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // VEHICLEMODEL
        // ADD GET
        public Vehicles AddModelGet()
        {
            Vehicles vehicles = new Vehicles();

            vehicles.vehicleMakeList = _db.VehicleMake.Select(s => new SelectListItem
            {
                Selected = false,
                Text = s.abrv,
                Value = s.id.ToString()
            });

            return vehicles;
        
        }

        // ADD POST
        public bool AddModel(ModelStateDictionary ModelState, Vehicles vehicles)
        {          
            if (ModelState.IsValid)
            {
                _db.VehicleModel.Add(vehicles.vehicleModel);
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // UPDATE/DELETE GET
        public async Task<VehicleModel?> UpdateDeleteModelGet(int? id)
        {
            var vehicleFromDb = await _db.VehicleModel.FindAsync(id);

            return vehicleFromDb;
        }

        // UPDATE POST
        public bool UpdatePostModel(ModelStateDictionary ModelState, VehicleModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.VehicleModel.Update(obj);
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // DELETE POST
        public async Task<bool> DeletePostModel(int? id)
        {
            var obj = await _db.VehicleModel.FindAsync(id);

            if (obj != null)
            {
                _db.VehicleModel.Remove(obj);
                _db.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
