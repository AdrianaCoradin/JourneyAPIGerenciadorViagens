using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody]RequestRegisterTripJson request)
        {
            var useCase = new RegisterTripUseCase();
            useCase.Execute(request);
            return Created();
        }
    }
}
