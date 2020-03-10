using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPWService.IRepository;
using IPWService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommonRequestModel.Request;
using CommonRequestModel.Response;

namespace IPWService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionalityController : ControllerBase
    {
        IFunctionalityRepository functionalityRepository;
        public FunctionalityController(IFunctionalityRepository _functionalityRepository)
        {
            functionalityRepository = _functionalityRepository;
        }
        [HttpGet]
        [Route("GetFunctionalties")]
        public async Task<IActionResult> GetFunctionalties()
        {
            try
            {
                var functionality = await functionalityRepository.GetFunctionalities();
                if (functionality == null)
                {
                    return NotFound(functionality);
                }
                return Ok(functionality);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetFunctionality")]
        public async Task<IActionResult> GetFunctionality(int? functionalityId)
        {
            try
            {
                var role = await functionalityRepository.GetFunctionality(functionalityId);
                if (role == null)
                {
                    return NotFound();
                }

                return Ok(role);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Addfunctionality")]
        public async Task<IActionResult> Addfunctionality([FromBody]FunctionalityRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var functionalityId = await functionalityRepository.AddFunctionality(new Functionalities() {
                        Name=model.Name,
                        StatusId=model.StatusId
                    });
                    if (functionalityId > 0)
                    {
                        return Ok(functionalityId);
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
        [Route("DeleteFunctionality")]
        public async Task<IActionResult> DeleteFunctionality(int? functionalityId)
        {
            int result = 0;

            if (functionalityId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await functionalityRepository.DeleteFunctionality(functionalityId);
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
        [Route("UpdateFunctionality")]
        public async Task<IActionResult> UpdateFunctionality(FunctionalityRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await functionalityRepository.UpdateFunctionality(new Functionalities() {
                        FunctionalityId=model.FunctionalityId,
                        Name=model.Name,
                        StatusId=model.StatusId
                    });

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