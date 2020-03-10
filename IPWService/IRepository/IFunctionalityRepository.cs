using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPWService.Models;

namespace IPWService.IRepository
{
    public interface IFunctionalityRepository
    {
        Task<List<Functionalities>> GetFunctionalities();

        Task<Functionalities> GetFunctionality(int? FunctionalityId);

        Task<int> AddFunctionality(Functionalities Functionality);

        Task<int> DeleteFunctionality(int? FunctionalityId);

        Task UpdateFunctionality(Functionalities Functionality);
    }
}
