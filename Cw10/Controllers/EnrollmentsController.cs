using System;
using Cw10.DTOs.Requests;
using Cw10.DTOs.Responses;
using Cw10.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw10.Controllers {
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase {
        private readonly IDbService _service;

        public EnrollmentsController(IDbService service) {
            _service = service;
        }
        
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request) {
            var response = _service.EnrollStudent(request);
            return Created(nameof(response), response);
        }
        
        [HttpPost("promotions")]
        public IActionResult EnrollPromotions(EnrollPromotionsRequest request) {
            _service.EnrollPromotions(request);
            return Created(nameof(request), request);
        }
    }
}