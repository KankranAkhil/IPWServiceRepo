using IPWService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPWService.IRepository
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetCompanies();

        Task<Company> GetCompany(int? companyId);

        Task<int> AddCompany(Company company);

        Task<int> DeleteCompany(int? companyId);

        Task UpdateCompany(Company company);
    }
}
