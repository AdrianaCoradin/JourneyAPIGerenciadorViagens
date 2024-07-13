using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Exception.ExceptionsBase;
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
            try
            {
                var useCase = new RegisterTripUseCase();
                var response = useCase.Execute(request);
                return Created(string.Empty, response);
            }
            //Só vai cair no catch exceções previstas na classe JourneyException
            catch (JourneyException ex)
            {
                return BadRequest(ex.Message);
            }
            //Se não tiver previstas na classe JourneyException, apresenta erro 500 mais mensagem
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var useCase = new GetAllTripsUseCase();
            var result = useCase.Execute();
            return Ok(result);
        }

    }
}
