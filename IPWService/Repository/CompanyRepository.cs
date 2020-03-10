using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using IPWService.Models;
using IPWService.IRepository;

namespace IPWService.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        IPW_DevContext _db;
        public CompanyRepository(IPW_DevContext db)
        {
            _db = db;
        }
        public async Task<int> AddCompany(Company company)
        {
            if (_db != null)
            {
                await _db.Company.AddAsync(new Company() {
                    Name=company.Name,
                    Address=company.Address
                });
                await _db.SaveChangesAsync();

                return company.CompanyId;
            }

            return 0;
        }

        public async Task<int> DeleteCompany(int? companyId)
        {
            int result = 0;

            if (_db != null)
            {
                //Find the post for specific post id
                var post = await _db.Company.FirstOrDefaultAsync(x => x.CompanyId == companyId);

                if (post != null)
                {
                    //Delete that post
                    _db.Company.Remove(post);

                    //Commit the transaction
                    result = await _db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }
        
        public async Task<List<Company>> GetCompanies()
        {
            if (_db != null)
            {
                return await _db.Company.ToListAsync();
            }

            return null;
        }

        public async Task<Company> GetCompany(int? companyId)
        {
            if (_db != null)
            {
                return await(from c in _db.Company
                             where c.CompanyId == companyId
                             select new Company
                             {
                                 CompanyId=c.CompanyId,
                                 Name=c.Name,
                                 Address=c.Address
                             }).FirstOrDefaultAsync();
            }

            return null;
        }
        
        public async Task UpdateCompany(Company company)
        {
            if (_db != null)
            {
                var existingCompany = _db.Company.Where(s => s.CompanyId == company.CompanyId).FirstOrDefault<Company>();
                if (existingCompany != null)
                {
                    existingCompany.Name = company.Name;
                    existingCompany.Address = company.Address;
                }
                await _db.SaveChangesAsync();
            }
        }


    }
}
