using CommonRequestModel.Response;
using IPWService.IRepository;
using IPWService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPWService.Repository
{
    public class FunctionalityRepository : IFunctionalityRepository
    {
        IPW_DevContext _db;
        public FunctionalityRepository(IPW_DevContext db)
        {
            _db = db;
        }
        public async Task<int> AddFunctionality(Functionalities Functionality)
        {
            if (_db != null)
            {
                await _db.Functionalities.AddAsync(new Functionalities() {
                    Name=Functionality.Name,
                    StatusId=Functionality.StatusId
                });
                await _db.SaveChangesAsync();

                return Functionality.FunctionalityId;
            }

            return 0;
        }

        public async Task<int> DeleteFunctionality(int? FunctionalityId)
        {
            int result = 0;

            if (_db != null)
            {
                //Find the post for specific post id
                var post = await _db.Functionalities.FirstOrDefaultAsync(x => x.FunctionalityId == FunctionalityId);

                if (post != null)
                {
                    //Delete that post
                    _db.Functionalities.Remove(post);

                    //Commit the transaction
                    result = await _db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<List<Functionalities>> GetFunctionalities()
        {
            if (_db != null)
            {
                
                return await _db.Functionalities.Include(s=>s.Status).Select(f => new Functionalities()
                {
                    FunctionalityId = f.FunctionalityId,
                    Name = f.Name,
                    StatusId = f.StatusId,

                    Status = f.Status == null ? null : new Status()
                    {
                        StatusId = f.Status.StatusId,
                        Name = f.Status.Name
                    }

                }).ToListAsync();
            }

            return null;
        }

        public async Task<Functionalities> GetFunctionality(int? FunctionalityId)
        {
            if (_db != null)
            {
                return await _db.Functionalities.Include(s=>s.Status).Include(m=>m.RoleFunctionMapping).Select(f=>new Functionalities() {
                    FunctionalityId=f.FunctionalityId,
                    Name=f.Name,
                    StatusId=f.StatusId,
                    
                    Status=f.Status==null?null:new Status()
                    {
                        StatusId=f.Status.StatusId,
                        Name=f.Status.Name
                    }

                }).FirstOrDefaultAsync(f => f.FunctionalityId == FunctionalityId);



            }

            return null;
        }

        public async Task UpdateFunctionality(Functionalities Functionality)
        {
            if (_db != null)
            {
                var existingFunctionality = _db.Functionalities.Where(s => s.FunctionalityId == Functionality.FunctionalityId).FirstOrDefault<Functionalities>();
                if (existingFunctionality != null)
                {
                    existingFunctionality.Name = Functionality.Name;
                    existingFunctionality.StatusId = Functionality.StatusId;
                }//Commit the transaction
                await _db.SaveChangesAsync();
            }
        }
    }
}
