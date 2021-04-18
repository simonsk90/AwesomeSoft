using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic;
using DTO;

namespace API.Controllers
{
    
    [RoutePrefix("api")]
    public class ParticipantController : ApiController
    {
        
        private readonly ParticipantService _participantService = new ParticipantService();
        
        [Route("Participant")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetParticipant()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        
        [Route("Participant")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateParticipant([FromBody] ParticipantDto participant)
        {
            await _participantService.CreateParticipant(participant);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
        
        [Route("Participant")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateLocation([FromBody] ParticipantDto participant)
        {
            await _participantService.UpdateParticipant(participant);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}