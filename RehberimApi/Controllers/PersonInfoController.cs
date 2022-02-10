using AppCore.Business.Models.Results;
using Business.Models;
using Business.Services.Bases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RehberimApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PersonInfoController : Controller {

        private readonly IUserInformationService _service;

        public PersonInfoController(IUserInformationService service) {
            _service = service;
        }

        [HttpPost("addpersoninfo")]
        public IActionResult AddPerson(UserInformationModel infoModel) {
            var result = _service.Add(infoModel);
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

        [HttpDelete("deletepersoninfo/{userId}")]
        public IActionResult DeletePersonInfo(int userId) {
            var result = _service.Delete(userId);
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
