using Microsoft.AspNetCore.Mvc;
using RoomBooking.App.Models;
using RoomBooking.App.Processors;

namespace RoomBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingController : ControllerBase
    {
        private IRoomBookingProcessor _processor;

        public RoomBookingController(IRoomBookingProcessor processor)
        {
            this._processor = processor;
        }

        [HttpPost]
        public async Task<IActionResult> BookRoom(RoomBookingRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = _processor.bookRoom(request);

                if (result.Flag == Tests.BookingResultFlag.Success)
                    return Ok(result);
                else
                    return BadRequest();
            }
            return BadRequest(ModelState);
        }
    }
}
