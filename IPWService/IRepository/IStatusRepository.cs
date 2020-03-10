using IPWService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPWService.IRepository
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetStatusList();

        Task<Status> GetStatus(int? StatusId);

        Task<int> AddStatus(Status Status);

        Task<int> DeleteStatus(int? StatusId);

        Task UpdateStatus(Status Status);
    }
}
