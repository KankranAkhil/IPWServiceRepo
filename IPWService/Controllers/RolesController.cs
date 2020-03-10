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
    public class RolesController : ControllerBase
    {
        IRolesRepository roleRepository;
        public RolesController(IRolesRepository _roleRepository)
        {
            roleRepository = _roleRepository;
        }
        [HttpGet]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await roleRepository.GetRoles();
                if (roles == null)
                {
                    return NotFound();
                }

                return Ok(roles);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetRole")]
        public async Task<IActionResult> GetRole(int? roleID)
        {
            try
            {
                var role = await roleRepository.GetRole(roleID);
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
        [Route("AddRole")]
        public async Task<IActionResult> AddRole([FromBody]Roles model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var roleId = await roleRepository.AddRole(model);
                    if (roleId > 0)
                    {
                        return Ok(roleId);
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
        [Route("DeleteRole")]
        public async Task<IActionResult> DeleteRole(int? roleId)
        {
            int result = 0;

            if (roleId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await roleRepository.DeleteRole(roleId);
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
        [Route("UpdateRole")]
        public async Task<IActionResult> UpdateRole(Roles model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await roleRepository.UpdateRole(model);

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