using System.Linq;
using Cw10.DTOs.Requests;
using Cw10.Models;
using Cw10.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw10.Controllers {
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase {
        private readonly IDbService _service;

        public StudentsController(IDbService service) {
            _service = service;
        }
        
        [HttpGet]
        public IActionResult GetStudents() {
            return Ok(_service.GetStudents());
        }

        [HttpPut]
        public IActionResult UpdateStudent(UpdateStudentRequest student) {
            _service.UpdateStudent(student);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteStudent(string index) {
            _service.DeleteStudent(index);
            return Ok();
        }
    }
}