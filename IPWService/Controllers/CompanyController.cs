using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPWService.IRepository;
using IPWService.Models;
using IPWService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPWService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        ICompanyRepository companyRepository;
        public CompanyController(ICompanyRepository _companyRepository)
        {
            companyRepository = _companyRepository;
        }
        [HttpGet]
        [Route("GetCompanies")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await companyRepository.GetCompanies();
                if (companies == null)
                {
                    return NotFound();
                }

                return Ok(companies);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetCompany")]
        public async Task<IActionResult> GetCompany(int? companyId)
        {
            try
            {
                var company = await companyRepository.GetCompany(companyId);
                if (company == null)
                {
                    return NotFound();
                }

                return Ok(company);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddCompany")]
        public async Task<IActionResult> AddCompany([FromBody]Company model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companyId = await companyRepository.AddCompany(model);
                    if (companyId > 0)
                    {
                        return Ok(companyId
);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPost]
        [Route("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(int? companyId)
        {
            int result = 0;

            if (companyId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await companyRepository.DeleteCompany(companyId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(Company model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await companyRepository.UpdateCompany(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}