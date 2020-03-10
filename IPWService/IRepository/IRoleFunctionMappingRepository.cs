using CommonRequestModel.Request;
using IPWService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPWService.IRepository
{
    public interface IRoleFunctionMappingRepository
    {
        Task<List<RoleFunctionMapping>> GetRoleFunctionMappingList();

        Task<RoleFunctionMapping> GetRoleFunctionMapping(int? RoleFunctionMappingId);

        Task<int> AddRoleFunctionMapping(RoleFunctionMappingRequest Role);

        Task<int> DeleteRoleFunctionMapping(int? RoleFunctionMappingId);

        Task UpdateRoleFunctionMapping(RoleFunctionMappingRequest Role);
    }
}
