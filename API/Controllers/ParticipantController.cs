using System;
using System.Collections.Generic;
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

        [Route("Participant/{participantId}")]
        [HttpGet]
        public async Task<ParticipantDto> GetParticipant(int participantId)
        {
            try
            {
                ParticipantDto result = await ParticipantService.GetParticipant(participantId);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [Route("Participant")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateParticipant([FromBody] ParticipantDto participant)
        {
            HttpResponseMessage result;
            try
            {
                await ParticipantService.CreateParticipant(participant);
                result = Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return result;
        }
        
        [Route("Participant")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateParticipant([FromBody] ParticipantDto participant)
        {
            HttpResponseMessage result;
            try
            {
                await ParticipantService.UpdateParticipant(participant);
                result = Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return result;
        }

        [Route("ParticipantMeetings/{participantId}")]
        [HttpGet]
        public async Task<List<MeetingDto>> GetMeetingsForParticipant(int participantId)
        {
            try
            {
                return await ParticipantService.GetMeetingsForParticipant(participantId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}