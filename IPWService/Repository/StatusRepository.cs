using IPWService.IRepository;
using IPWService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPWService.Repository
{
    public class StatusRepository : IStatusRepository
    {
        IPW_DevContext _db;
        public StatusRepository(IPW_DevContext db)
        {
            _db = db;
        }
        public async Task<int> AddStatus(Models.Status Status)
        {
            if (_db != null)
            {
                await _db.Status.AddAsync(new Status()
                {
                    Name = Status.Name
                });
                await _db.SaveChangesAsync();

                return Status.StatusId;
            }

            return 0;
        }

        public async Task<int> DeleteStatus(int? StatusId)
        {
            int result = 0;

            if (_db != null)
            {
                //Find the post for specific post id
                var post = await _db.Status.FirstOrDefaultAsync(x => x.StatusId == StatusId);

                if (post != null)
                {
                    //Delete that post
                    _db.Status.Remove(post);

                    //Commit the transaction
                    result = await _db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<Models.Status> GetStatus(int? StatusId)
        {
            if (_db != null)
            {
                return await _db.Status.Select(s=>new Status() {
                    StatusId=s.StatusId,
                    Name=s.Name
                }).Where(s=>s.StatusId== StatusId).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<Models.Status>> GetStatusList()
        {
            if (_db != null)
            {
                return await _db.Status.Select(s => new Status()
                {
                    StatusId = s.StatusId,
                    Name = s.Name
                }).ToListAsync();
            }

            return null;
        }

        public async Task UpdateStatus(Models.Status Status)
        {
            if (_db != null)
            {
                var existingStatus = _db.Status.Where(s => s.StatusId == Status.StatusId).FirstOrDefault<Status>();
                if (existingStatus != null)
                {
                    existingStatus.Name = Status.Name;
                }
                //Commit the transaction
                await _db.SaveChangesAsync();
            }
        }
    }
}
