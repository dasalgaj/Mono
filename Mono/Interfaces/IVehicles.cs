﻿using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mono.Models;

namespace Mono.Interfaces
{
    public interface IVehicles
    {
        //VEHICLEMAKE
        bool AddMake(ModelStateDictionary ModelState, VehicleMake vehicleMake);
        Task<VehicleMake?> UpdateDeleteMakeGet(int? id);
        bool UpdatePostMake(ModelStateDictionary ModelState, VehicleMake vehicleMake);
        Task<bool> DeletePostMake(int? id);

        //VEHICLEMODEL
        Vehicles AddModelGet();
        bool AddModel(ModelStateDictionary ModelState, Vehicles vehicles);
        Task<VehicleModel?> UpdateDeleteModelGet(int? id);
        bool UpdatePostModel(ModelStateDictionary ModelState, VehicleModel vehicleMake);
        Task<bool> DeletePostModel(int? id);
    }
}
