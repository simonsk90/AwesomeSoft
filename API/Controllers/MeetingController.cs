using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic;
using DTO;

namespace API.Controllers
{
    
    
    
    [RoutePrefix("api")]
    public class MeetingController : ApiController
    {
        private readonly MeetingService _meetingService = new MeetingService();
        
        [Route("Meeting/{meetingId}")]
        [HttpGet]
        public async Task<MeetingDto> GetMeetingById(int meetingId)
        {
            return await _meetingService.GetMeetingById(meetingId);
        }
        
        [Route("Meetings")]
        [HttpGet]
        public async Task<List<MeetingDto>> GetAllMeetings()
        {
            return await _meetingService.GetAllMeetings();
        }
    }
}