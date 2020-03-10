using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonRequestModel.Request;
using IPWService.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPWService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleFunctionMappingController : ControllerBase
    {
        IRoleFunctionMappingRepository roleFunctionMappingRepository;
        public RoleFunctionMappingController(IRoleFunctionMappingRepository _roleFunctionMappingRepository)
        {
            roleFunctionMappingRepository = _roleFunctionMappingRepository;
        }
        [HttpGet]
        [Route("GetRoleFunctionMappingList")]
        public async Task<IActionResult> GetRoleFunctionMappingList()
        {
            try
            {
                var RoleFunctionMapping = await roleFunctionMappingRepository.GetRoleFunctionMappingList();
                if (RoleFunctionMapping == null)
                {
                    return NotFound();
                }

                return Ok(RoleFunctionMapping);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetRoleFunctionMapping")]
        public async Task<IActionResult> GetRoleFunctionMapping(int? statusId)
        {
            try
            {
                var RoleFunctionMapping = await roleFunctionMappingRepository.GetRoleFunctionMapping(statusId);
                if (RoleFunctionMapping == null)
                {
                    return NotFound();
                }

                return Ok(RoleFunctionMapping);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddRoleFunctionMapping")]
        public async Task<IActionResult> AddRoleFunctionMapping([FromBody]RoleFunctionMappingRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var RoleFunctionMappingId = await roleFunctionMappingRepository.AddRoleFunctionMapping(model);
                    if (RoleFunctionMappingId > 0)
                    {
                        return Ok(RoleFunctionMappingId);
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
        [Route("DeleteStatus")]
        public async Task<IActionResult> DeleteRoleFunctionMapping(int? RoleFunctionMappingId)
        {
            int result = 0;

            if (RoleFunctionMappingId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await roleFunctionMappingRepository.DeleteRoleFunctionMapping(RoleFunctionMappingId);
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
        [Route("UpdateRoleFunctionMapping")]
        public async Task<IActionResult> UpdateRoleFunctionMapping(RoleFunctionMappingRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await roleFunctionMappingRepository.UpdateRoleFunctionMapping(model);

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