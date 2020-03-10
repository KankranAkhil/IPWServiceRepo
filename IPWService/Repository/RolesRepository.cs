using IPWService.IRepository;
using IPWService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPWService.Repository
{
    public class RolesRepository : IRolesRepository
    {
        IPW_DevContext _db;
        public RolesRepository(IPW_DevContext db)
        {
            _db = db;
        }
        public async Task<int> AddRole(Models.Roles Role)
        {
            if (_db != null)
            {
                await _db.Roles.AddAsync(new Roles()
                {
                    Name = Role.Name,
                    StatusId = Role.StatusId

                });
                await _db.SaveChangesAsync();

                return Role.RoleId;
            }

            return 0;
        }

        public async Task<int> DeleteRole(int? RoleId)
        {
            int result = 0;

            if (_db != null)
            {
                //Find the post for specific post id
                var post = await _db.Roles.FirstOrDefaultAsync(x => x.RoleId == RoleId);

                if (post != null)
                {
                    //Delete that post
                    _db.Roles.Remove(post);

                    //Commit the transaction
                    result = await _db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<List<Models.Roles>> GetRoles()
        {
            if (_db != null)
            {
                return await _db.Roles.Include(s=>s.Status).Select(
                    r=>new Roles() {
                        RoleId= r.RoleId,
                        Name=r.Name,
                        Status= r.Status==null?null:new Status()
                        {
                            StatusId=r.Status.StatusId,
                            Name=r.Status.Name
                        }
                    }
                    ).ToListAsync();
            }

            return null;
        }

        public async Task<Models.Roles> GetRole(int? RoleId)
        {
            if (_db != null)
            {
                return await (from c in _db.Roles

                              where c.RoleId == RoleId
                              select new Roles
                              {
                                  RoleId = c.RoleId,
                                  Name = c.Name,
                                  Status = c.Status==null?null:new Status()
                                  {
                                      StatusId=c.Status.StatusId,
                                      Name=c.Status.Name
                                  }
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task UpdateRole(Models.Roles Role)
        {
            if (_db != null)
            {
                var existingRole = _db.Roles.Where(s => s.RoleId == Role.RoleId).FirstOrDefault<Roles>();
                if (existingRole != null)
                {
                    existingRole.Name = Role.Name;
                    existingRole.StatusId = Role.RoleId;
                }
                await _db.SaveChangesAsync();
            }
        }
    }
}
