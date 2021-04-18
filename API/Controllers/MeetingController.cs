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
    public class MeetingController : ApiController
    {

        [Route("Meeting/{meetingId}")]
        [HttpGet]
        public async Task<MeetingDto> GetMeetingById(int meetingId)
        {
            try
            {
                return await MeetingService.GetMeetingById(meetingId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [Route("Meetings")]
        [HttpGet]
        public async Task<List<MeetingDto>> GetAllMeetings()
        {
            try
            {
                return await MeetingService.GetAllMeetings();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [Route("MeetingComplications")]
        [HttpGet]
        public async Task GetMeetingComplications()
        {
            try
            {
                await MeetingService.GetComplications();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("Meeting")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateMeeting([FromBody]MeetingDto meetingDto)
        {
            HttpResponseMessage result;
            try
            {
                await MeetingService.CreateMeeting(meetingDto);
                result = Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return result;
        }
        
        [Route("Meeting")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateMeeting([FromBody]MeetingDto meetingDto)
        {
            HttpResponseMessage result;
            try
            {
                await MeetingService.UpdateMeeting(meetingDto);
                result = Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return result;
        }

        
        [Route("AddParticipantToMeeting/{participantId}/{meetingId}")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddParticipantToMeeting(int participantId, int meetingId)
        {
            HttpResponseMessage result;
            try
            {
                await MeetingService.AddPersonToMeeting(participantId, meetingId);
                result = Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return result;
        }
        
        
    }
}