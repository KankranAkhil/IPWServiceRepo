using IPWService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPWService.IRepository
{
    public interface IRolesRepository
    {
        Task<List<Roles>> GetRoles();

        Task<Roles> GetRole(int? RoleId);

        Task<int> AddRole(Roles Role);

        Task<int> DeleteRole(int? RoleId);
        Task UpdateRole(Roles Role);
    }
}
