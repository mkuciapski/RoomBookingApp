using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomBooking.API.Controllers;
using RoomBooking.App.Models;
using RoomBooking.App.Processors;
using RoomBooking.Tests;
using Shouldly;

namespace RoomBooking.API.Tests
{
    public class RoomBookingControllerTests
    {
        private Mock<IRoomBookingProcessor> _processor;
        private RoomBookingController _controller;
        private RoomBookingRequest _request;
        private RoomBookingResult _result;

        public RoomBookingControllerTests()
        {
            _processor = new Mock<IRoomBookingProcessor>();
            _controller = new RoomBookingController(_processor.Object);
            _request = new RoomBookingRequest();
            _result = new RoomBookingResult();

            _processor.Setup(x => x.bookRoom(_request)).Returns(_result);
        }

        [Theory]
        [InlineData(1, true, typeof(OkObjectResult), BookingResultFlag.Success)]
        [InlineData(0, false, typeof(BadRequestObjectResult), BookingResultFlag.Failure)]
        public async Task ShouldCallBookingMethodWhenValid(int expectedMethodCalls, bool isModelValid, Type expectedActionResultType, BookingResultFlag resultFlag)
        {
            if (!isModelValid)
                _controller.ModelState.AddModelError("Key", "Error");

            _result.Flag = resultFlag;

            var result = await _controller.BookRoom(_request);

            result.ShouldBeOfType(expectedActionResultType);
            _processor.Verify(x => x.bookRoom(_request), Times.Exactly(expectedMethodCalls));
        }
    }
}