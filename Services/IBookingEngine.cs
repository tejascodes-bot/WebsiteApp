using WebsiteApp.Models;

namespace WebsiteApp.Services
{
    public interface IBookingEngine
    {
        decimal CalculateTotalPrice(
            RoomType roomType,
            DateTime checkIn,
            DateTime checkOut);

        bool ValidateDates(
            DateTime checkIn,
            DateTime checkOut,
            out string errorMessage);

        Task<bool> IsRoomTypeAvailableAsync(
            int roomTypeId,
            DateTime checkIn,
            DateTime checkOut,
            CancellationToken cancellationToken = default);
    }
}