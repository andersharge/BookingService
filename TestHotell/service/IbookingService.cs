using System;

namespace TestHotell.service
{
  public interface IBookingService
  {

 
    void AddGuestToBooking(Guid bookingId, BookingService.Guest guest);
  }
}
