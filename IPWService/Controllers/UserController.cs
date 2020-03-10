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
    public class UserController : ControllerBase
    {
        IUsersRepository userRepository;
        public UserController(IUsersRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await userRepository.GetUsers();
                if (users == null)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(int? userId)
        {
            try
            {
                var user = await userRepository.GetUser(userId);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody]Users model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = await userRepository.AddUser(model);
                    if (userId > 0)
                    {
                        return Ok(userId);
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
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int? userId)
        {
            int result = 0;

            if (userId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await userRepository.DeleteUser(userId);
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
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Users model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await userRepository.UpdateUser(model);

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