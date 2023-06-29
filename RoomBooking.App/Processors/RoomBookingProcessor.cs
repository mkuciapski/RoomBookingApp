using RoomBooking.App.Models;
using RoomBooking.App.Services;
using RoomBooking.Domain;
using RoomBooking.Domain.BaseModels;

namespace RoomBooking.App.Processors
{
    public class RoomBookingProcessor
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingProcessor(IRoomBookingService roomBookingService)
        {
            this._roomBookingService = roomBookingService;
        }

        public RoomBookingResult bookRoom(RoomBookingRequest? bookingRequest)
        {
            if (bookingRequest is null)
                throw new ArgumentNullException(nameof(bookingRequest));

            var availableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date.Value);

            var result = createRoomBookingObject<RoomBookingResult>(bookingRequest);

            if (availableRooms.Any())
            {
                var room = availableRooms.FirstOrDefault();
                var roomBooking = createRoomBookingObject<RoomBookingData>(bookingRequest);
                roomBooking.RoomID = room.ID;
                _roomBookingService.Save(roomBooking);

                result.ID = roomBooking.ID;
                result.Flag = Tests.BookingResultFlag.Success;
            }
            else
                result.Flag = Tests.BookingResultFlag.Failure;

            return result;
        }

        private TRoomBooking createRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest) where TRoomBooking : RoomBookingBase, new()
        {
            return new TRoomBooking
            {
                FullName = bookingRequest?.FullName,
                Email = bookingRequest?.Email,
                Date = bookingRequest?.Date
            };
        }
    }
}