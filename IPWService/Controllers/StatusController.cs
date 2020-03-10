using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPWService.IRepository;
using IPWService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPWService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        IStatusRepository statusRepository;
        public StatusController(IStatusRepository _statusRepository)
        {
            statusRepository = _statusRepository;
        }
        [HttpGet]
        [Route("GetStatusList")]
        public async Task<IActionResult> GetStatusList()
        {
            try
            {
                var companies = await statusRepository.GetStatusList();
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
        [Route("GetStatus")]
        public async Task<IActionResult> GetStatus(int? statusId)
        {
            try
            {
                var status = await statusRepository.GetStatus(statusId);
                if (status == null)
                {
                    return NotFound();
                }

                return Ok(status);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddStatus")]
        public async Task<IActionResult> AddStatus([FromBody]Status model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var statusId = await statusRepository.AddStatus(model);
                    if (statusId > 0)
                    {
                        return Ok(statusId);
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
        public async Task<IActionResult> DeleteStatus(int? statusId)
        {
            int result = 0;

            if (statusId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await statusRepository.DeleteStatus(statusId);
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
        [Route("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(Status model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await statusRepository.UpdateStatus(model);

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