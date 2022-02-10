using AppCore.Business.Models.Results;
using Business.Models;
using Business.Services.Bases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RehberimApp.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class PersonOperationController : Controller {
        IUserService _userService;
        public PersonOperationController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet("getpersons")]
        public IActionResult GetPersons() {
            var result = _userService.GetQuery();
            if (result.Status == ResultStatus.Success) {
                return Ok(result);
            }
            if (result.Status == ResultStatus.Error) {
                return BadRequest(result.Message);
            }
            if (result.Status == ResultStatus.Exception) {
                return BadRequest(result.Message);
            }
            return BadRequest("hatalı istek");
        }

        [HttpPost("addperson")]
        public IActionResult AddPerson(UsersModel usersModel) {
            var result = _userService.Add(usersModel);
                if (result.Status==ResultStatus.Success) {  
                return Ok(result);
            }
            if (result.Status==ResultStatus.Error) {
                return BadRequest(result.Message);
            }
            if (result.Status==ResultStatus.Exception) {
                return BadRequest(result.Message);
            }
            return BadRequest("hatalı istek");
        }

        [HttpDelete ("deleteperson/{userId}")]
        public IActionResult DeletePerson(int userId) {
            if (userId != 0) {
                var result = _userService.Delete(userId);
                if (result.Status == ResultStatus.Success) {
                    return Ok(result);
                }
                if (result.Status == ResultStatus.Error) {
                    return BadRequest(result.Message);
                }
                if (result.Status == ResultStatus.Exception) {
                    return BadRequest(result.Message);
                }
                return BadRequest("hatalı istek");
            }
            return BadRequest("UserId gönderilmelidir.");
        }

        [HttpPut("ignoreperson")]
        public IActionResult IgnorePerson(Dictionary<string, int> id) {
            int realId = 0;
            id.TryGetValue("id", out realId);
            if (realId != 0) {
              var result= _userService.IgnoreUser(realId);
                if (result.Status == ResultStatus.Success) {
                    return Ok(result);
                }
                if (result.Status == ResultStatus.Error) {
                    return BadRequest(result.Message);
                }
                if (result.Status == ResultStatus.Exception) {
                    return BadRequest(result.Message);
                }
                return BadRequest("hatalı istek");
            }
            return BadRequest("Bir şeyler yanlış gitti.");
        }

        [HttpPut("updateperson")]
        public IActionResult UpdatePerson(UsersModel usersModel) {
            var result = _userService.Update(usersModel);
            if (result.Status == ResultStatus.Success) {
                return Ok(result);
            }
            if (result.Status == ResultStatus.Error) {
                return BadRequest(result.Message);
            }
            if (result.Status == ResultStatus.Exception) {
                return BadRequest(result.Message);
            }
            return BadRequest("hatalı istek");
        }

        [HttpGet("getuserbyid/{userid}")]
        public IActionResult UpdatePerson(int userId) {
            var result = _userService.GetUserById(userId);
            if (result.Status == ResultStatus.Success) {
                return Ok(result);
            }
            if (result.Status == ResultStatus.Error) {
                return BadRequest(result.Message);
            }
            if (result.Status == ResultStatus.Exception) {
                return BadRequest(result.Message);
            }
            return BadRequest("hatalı istek");
        }
    }
}
