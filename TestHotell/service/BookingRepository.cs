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
      return true;
    }

    public string ReEnterBookingInformation(string message)
    {
      return message;
    }
    
  }
}
