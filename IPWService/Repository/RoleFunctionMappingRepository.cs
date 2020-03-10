using CommonRequestModel.Request;
using IPWService.IRepository;
using IPWService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPWService.Repository
{
    public class RoleFunctionMappingRepository : IRoleFunctionMappingRepository
    {
        IPW_DevContext _db;
        public RoleFunctionMappingRepository(IPW_DevContext db)
        {
            _db = db;
        }
        public async Task<int> AddRoleFunctionMapping(RoleFunctionMappingRequest Role)
        {
            if (_db != null)
            {
                await _db.RoleFunctionMapping.AddAsync(new RoleFunctionMapping()
                {
                    RoleId = Role.RoleId,
                    FunctionId=Role.FunctionId,
                    StatusId=Role.StatusId
                });
                await _db.SaveChangesAsync();

                return 1;
            }

            return 0;
        }

        public async Task<int> DeleteRoleFunctionMapping(int? RoleFunctionMappingId)
        {
            int result = 0;

            if (_db != null)
            {
                //Find the post for specific post id
                var post = await _db.RoleFunctionMapping.FirstOrDefaultAsync(x => x.Id == RoleFunctionMappingId);

                if (post != null)
                {
                    //Delete that post
                    _db.RoleFunctionMapping.Remove(post);

                    //Commit the transaction
                    result = await _db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<Models.RoleFunctionMapping> GetRoleFunctionMapping(int? RoleFunctionMappingId)
        {
            if (_db != null)
            {
                return await _db.RoleFunctionMapping.Include(s => s.Status).Include(s => s.Role).Include(s => s.Function).Select(f => new RoleFunctionMapping()
                {
                    Id = f.Id,
                    FunctionId = f.FunctionId,
                    RoleId = f.RoleId,
                    StatusId = f.StatusId,

                    Status = f.Status == null ? null : new Status()
                    {
                        StatusId = f.Status.StatusId,
                        Name = f.Status.Name
                    },
                    Role = f.Role == null ? null : new Roles()
                    {
                        RoleId = f.Role.RoleId,
                        Name = f.Role.Name
                    },
                    Function = f.Function == null ? null : new Functionalities()
                    {
                        FunctionalityId = f.Function.FunctionalityId,
                        Name = f.Function.Name
                    }

                }).FirstOrDefaultAsync(f => f.Id == RoleFunctionMappingId);



            }

            return null;
        }

        public async Task<List<Models.RoleFunctionMapping>> GetRoleFunctionMappingList()
        {
            if (_db != null)
            {

                return await _db.RoleFunctionMapping.Include(s => s.Status).Include(s=>s.Role).Include(s=>s.Function).Select(f => new RoleFunctionMapping()
                {
                    Id=f.Id,
                    FunctionId = f.FunctionId,
                    RoleId = f.RoleId,
                    StatusId = f.StatusId,

                    Status = f.Status == null ? null : new Status()
                    {
                        StatusId = f.Status.StatusId,
                        Name = f.Status.Name
                    },
                    Role= f.Role==null?null:new Roles()
                    {
                        RoleId=f.Role.RoleId,
                        Name=f.Role.Name
                    },
                    Function= f.Function==null?null:new Functionalities()
                    {
                        FunctionalityId=f.Function.FunctionalityId,
                        Name=f.Function.Name
                    }

                }).ToListAsync();
            }

            return null;
        }

        public async Task UpdateRoleFunctionMapping(RoleFunctionMappingRequest RoleFunctionMapping)
        {
            if (_db != null)
            {
                var existingMapping = _db.RoleFunctionMapping.Where(s => s.Id == RoleFunctionMapping.Id).FirstOrDefault<Models.RoleFunctionMapping>();
                if (existingMapping != null)
                {
                    existingMapping.RoleId = RoleFunctionMapping.RoleId;
                    existingMapping.FunctionId = RoleFunctionMapping.FunctionId;
                    existingMapping.StatusId = RoleFunctionMapping.StatusId;
                }
                await _db.SaveChangesAsync();
            }
        }
    }
}
