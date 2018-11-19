using System;

namespace TestHotell.service
{
  public interface IBookingRepository
  {
    BookingService.Booking GetBooking(Guid guid);
    bool Save(BookingService.Booking booking, BookingService.Guest gust);
    string ReEnterGuestInformation(string message);
  }
}
