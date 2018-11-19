using System;
using System.Collections.Generic;
using System.Linq;

namespace TestHotell.service
{
  public class BookingService : IBookingService
  {
    private readonly IBookingRepository _iBookingRepository;
    public BookingService(IBookingRepository IBookingRepository)
    {
      _iBookingRepository = IBookingRepository;
    }

    public enum Country
    {
      SE,
      DK,
      DE
    }
    public enum RoomTyp
    {
      SINGLE,
      DOUBLE,
      TWIN,
      TRIPLE
    }
  

    public class Hotel
    {
      public string Name { get; set; }
      public Country CountryCode { get; set; }
    }

    public string error { get; set; }

    public class Booking
    {
      public IEnumerable<Guest> Guests { get; set; }
      public string RoomType { get; set; }
      public Hotel Hotel { get; set; }
    }

    public class Guest
    {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Title { get;set;}
    }

    private Booking FetchBooking(Guid bookingId)
    {
      var getbooking = _iBookingRepository.GetBooking(bookingId);
      return getbooking;
    }
    
    public void AddGuestToBooking(Guid bookingId, Guest guest)
    {
      try
      {
        Booking currentBooking = FetchBooking(Guid.NewGuid());

        int getNumberOfGuest = currentBooking.Guests.Count() + 1;
        var roomTyp = currentBooking.RoomType;

        if (!CheckRoomTypeAndNumberOfGuest(getNumberOfGuest, roomTyp))
        {
          error = _iBookingRepository.ReEnterBookingInformation($"Tyvärr fungerar inte rummet med {getNumberOfGuest} gäster");
        }

        var currentCountryCode = currentBooking.Hotel.CountryCode.ToString();
        var title = guest.Title.ToLower();
     
        if (string.IsNullOrEmpty(title) && currentCountryCode == "DE")
        {
          error = _iBookingRepository.ReEnterBookingInformation($"Titel är obligatorisk på tyska gäster");
        }

        var getTitels = NameTitle();

        if (currentCountryCode.ToUpper() == Country.DE.ToString())
        {
          if (!(getTitels.Any(x => x.Item1 == title && x.Item3)))
          {
            error = _iBookingRepository.ReEnterBookingInformation($"Tyska hotell måste ha följande titel på gästen");
          }

        }

        _iBookingRepository.Save(currentBooking,guest);

      }
      catch (Exception e)
      {
        Console.WriteLine(e);
       
      }

    }

    public bool CheckRoomTypeAndNumberOfGuest(int numberOfGuest,string currentRoomTyp)
    {
      if (string.IsNullOrEmpty(currentRoomTyp))
      {
        return false;
      }
      
      var romType = RomType();
      
      bool checksRoomTyp = romType.Any(t => t.Item1.ToLower() == currentRoomTyp.ToLower() &&
          numberOfGuest <= t.Item2);

      return checksRoomTyp;
    }
    /// <summary>
    /// This method check if  NameTitle is contain any 
    /// 
    /// </summary>
    /// <returns></returns>
    private static Tuple<string,Enum,bool>[] NameTitle()
    {
      Tuple<string, Enum,bool>[] titelName =
      {
        new Tuple<string, Enum,bool>("herr",Country.DE,true ),
        new Tuple<string, Enum,bool>("frau",Country.DE,true),
        new Tuple<string, Enum,bool>("dr",Country.DE,true),
      };
      return titelName;
    }


    private static Tuple<string, int>[] RomType()
    {
      Tuple<string, int>[] romType =
      {
        new Tuple<string, int>("SINGLE", 1),
        new Tuple<string, int>("DOUBLE", 2),
        new Tuple<string, int>("TWIN", 2),
        new Tuple<string, int>("TRIPLE", 3)
      };
      return romType;
    }
  }

}

