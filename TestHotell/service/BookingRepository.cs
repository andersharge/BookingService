using System;

namespace TestHotell.service
{
  public class BookingRepository:IBookingRepository
  {
    public BookingService.Booking GetBooking(Guid guid)
    {
      throw new NotImplementedException();
    }

    public bool Save(BookingService.Booking booking, BookingService.Guest gust)
    {
      //add a new guest to array of guest
      return true;
    }

    public string ReEnterGuestInformation(string message)
    {
      return message;
    }
    
  }
}
