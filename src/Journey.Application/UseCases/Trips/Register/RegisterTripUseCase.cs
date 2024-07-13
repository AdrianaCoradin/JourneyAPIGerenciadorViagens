using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);

            var dbContext = new JourneyDbContext();

            var entity = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            dbContext.Trips.Add(entity);

            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                EndDate = entity.EndDate,
                StartDate   = entity.StartDate,
                Name = entity.Name,
                Id = entity.Id
            };
        }

        // Validações referentes aos atributos da classe RequestRegisterTripJson.cs
        // Usamos JourneyException.cs que é uma classe de exceções próprias
        // Concentramos as mensagens de erro no arquivo ResourceErrorMessages.resx

        private void Validate(RequestRegisterTripJson request)
        {
            // Se o nome for vazio ou nulo
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new JourneyException(ResourceErrorMessages.NAME_EMPTY);
            }

            // Inicio da viagem não pode ser menor que hoje. Utc now é referente a data base do país. ignoramos o horário adicionando .Date.
            if(request.StartDate.Date < DateTime.UtcNow.Date)
            {
                throw new JourneyException(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
            }

            // Fim da viagem não pode ser menor que a data de inicio
            if (request.EndDate.Date < request.StartDate.Date)
            {
                throw new JourneyException(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);
            }

        }
    }
}
