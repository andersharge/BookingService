using System;
using System.Collections.Generic;
using TestHotell.service;
using Xunit;
using Moq;

namespace XUnitTestProject1
{
  [Trait("Category", "BookingService")]
  public class UnitTestHotellApi
  {
    private readonly Mock<IBookingRepository> _mockiBookingRepository;
    private readonly BookingService _sut;

    public UnitTestHotellApi()
    {
      _mockiBookingRepository=new Mock<IBookingRepository>();

      _sut=new BookingService(_mockiBookingRepository.Object);
    }


    [Fact(DisplayName = "Check if roomTyp fit number of guest")]
    public void CheckIfRoomTypFitNumberOfGuest()
    {
      var result =_sut.CheckRoomTypeAndNumberOfGuest(2, "DOUBLE");
      Assert.True(result,"2 stycken är max för ett dubbelrum");
      result = _sut.CheckRoomTypeAndNumberOfGuest(3, "DOUBLE");
      Assert.False(result,"3 stycken i detta rum är förmånmga");
      result = _sut.CheckRoomTypeAndNumberOfGuest(1, "dOUBLE");
      Assert.True(result,"i ett dubelrum så ryms en person också");
      result = _sut.CheckRoomTypeAndNumberOfGuest(1, null);
      Assert.False(result, "skickar man in fel rummstyp eller null så blir det falskt");
      result = _sut.CheckRoomTypeAndNumberOfGuest(100, null);
      Assert.False(result, "Sådana rumm finns inte");
    }


    [Fact(DisplayName = "Test_of_AddGuestToBooking_hotell_DE_title_Herr")]
    public void Test_of_AddGuestToBooking_hotell_DE_title_Herr()
    {
     
      _mockiBookingRepository.Setup(x => x.GetBooking(It.IsAny<Guid>())).Returns(new BookingService.Booking
      {
       Hotel = new BookingService.Hotel
       {
         CountryCode = BookingService.Country.DE,
         Name = "Hotell connect"
       },
        Guests = new List<BookingService.Guest>{
          new BookingService.Guest()
          {
            Title = "Tryne",
            FirstName = "Anders",
            LastName = "Harge"
          }
        },
        RoomType =BookingService.RoomTyp.DOUBLE.ToString()
      });

    var addedGuest =  new BookingService.Guest
      {
        FirstName = "Patrik",
        LastName = "Henriksson",
        Title = "Herr"
      };
      _sut.AddGuestToBooking(Guid.NewGuid(), addedGuest);
    }



    [Fact(DisplayName = "Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_DE_title_notset")]
    public void Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_DE_title_notset()
    {
      _mockiBookingRepository.Setup(x => x.GetBooking(It.IsAny<Guid>())).Returns(new BookingService.Booking
      {
        Hotel = new BookingService.Hotel
        {
          CountryCode = BookingService.Country.DE,
          Name = "Hotell connect"
        },
        Guests = new List<BookingService.Guest>{
          new BookingService.Guest()
          {
            Title = "VD",
            FirstName = "Anders",
            LastName = "Harge"
          }
        },
        RoomType = BookingService.RoomTyp.DOUBLE.ToString()
      });

      var addedGuest = new BookingService.Guest
      {
        FirstName = "Patrik",
        LastName = "Henriksson",
        Title = ""
      };

     _mockiBookingRepository.Setup(x => x.ReEnterBookingInformation(It.IsAny<string>())).Throws<Exception>();
      
      _sut.AddGuestToBooking(Guid.NewGuid(), addedGuest);
      
      _mockiBookingRepository.Verify(x=>x.ReEnterBookingInformation(It.IsAny<string>()),Times.AtLeastOnce());
      _mockiBookingRepository.Verify(x => x.Save(It.IsAny<BookingService.Booking>()
        ,It.IsAny<BookingService.Guest>()), Times.Never);
    }

    [Fact(DisplayName = "Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_DE_valid_title_herr")]
    public void Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_DE_valid_title_herr()
    {
      _mockiBookingRepository.Setup(x => x.GetBooking(It.IsAny<Guid>())).Returns(new BookingService.Booking
      {
        Hotel = new BookingService.Hotel
        {
          CountryCode = BookingService.Country.DE,
          Name = "Hotell connect"
        },
        Guests = new List<BookingService.Guest>{
          new BookingService.Guest()
          {
            Title = "VD",
            FirstName = "Anders",
            LastName = "Harge"
          }
        },
        RoomType = BookingService.RoomTyp.DOUBLE.ToString()
      });

      var addedGuest = new BookingService.Guest
      {
        FirstName = "Patrik",
        LastName = "Henriksson",
        Title = "Herr"
      };

      _mockiBookingRepository.Setup(x => x.ReEnterBookingInformation(It.IsAny<string>())).Throws<Exception>();

      _sut.AddGuestToBooking(Guid.NewGuid(), addedGuest);

      _mockiBookingRepository.Verify(x => x.ReEnterBookingInformation(It.IsAny<string>()), Times.Never);
      _mockiBookingRepository.Verify(x => x.Save(It.IsAny<BookingService.Booking>()
        , It.IsAny<BookingService.Guest>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_Se_valid_title_SpringPojke")]
    public void Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_Se_valid_title_SpringPojke()
    {
      _mockiBookingRepository.Setup(x => x.GetBooking(It.IsAny<Guid>())).Returns(new BookingService.Booking
      {
        Hotel = new BookingService.Hotel
        {
          CountryCode = BookingService.Country.SE,
          Name = "Hotell connect"
        },
        Guests = new List<BookingService.Guest>{
          new BookingService.Guest()
          {
            Title = "VD",
            FirstName = "Anders",
            LastName = "Harge"
          }
        },
        RoomType = BookingService.RoomTyp.DOUBLE.ToString()
      });

      var addedGuest = new BookingService.Guest
      {
        FirstName = "Patrik",
        LastName = "Henriksson",
        Title = "SpringPojke"
      };

      _mockiBookingRepository.Setup(x => x.ReEnterBookingInformation(It.IsAny<string>())).Throws<Exception>();

      _sut.AddGuestToBooking(Guid.NewGuid(), addedGuest);

      _mockiBookingRepository.Verify(x => x.ReEnterBookingInformation(It.IsAny<string>()), Times.Never);
      _mockiBookingRepository.Verify(x => x.Save(It.IsAny<BookingService.Booking>()
        , It.IsAny<BookingService.Guest>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_To_Small_Room")]
    public void Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_To_Small_Room()
    {
      _mockiBookingRepository.Setup(x => x.GetBooking(It.IsAny<Guid>())).Returns(new BookingService.Booking
      {
        Hotel = new BookingService.Hotel
        {
          CountryCode = BookingService.Country.SE,
          Name = "Hotell connect"
        },
        Guests = new List<BookingService.Guest>{
          new BookingService.Guest()
          {
            Title = "VD",
            FirstName = "Anders",
            LastName = "Harge"
          },
          new BookingService.Guest()
          {
            Title = "herr",
            FirstName = "Per",
            LastName = "olofsson"
          }
        },
        RoomType = BookingService.RoomTyp.DOUBLE.ToString()
      });

      var addedGuest = new BookingService.Guest
      {
        FirstName = "Patrik",
        LastName = "Henriksson",
        Title = "SpringPojke"
      };

      _mockiBookingRepository.Setup(x => x.ReEnterBookingInformation(It.IsAny<string>())).Throws<Exception>();

      _sut.AddGuestToBooking(Guid.NewGuid(), addedGuest);

      _mockiBookingRepository.Verify(x => x.ReEnterBookingInformation(It.IsAny<string>()), Times.AtLeastOnce);
      _mockiBookingRepository.Verify(x => x.Save(It.IsAny<BookingService.Booking>()
        , It.IsAny<BookingService.Guest>()), Times.Never);
    }


    [Fact(DisplayName = "Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_To_Room_3_beeds")]
    public void Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_To_Room_3_beeds()
    {
      _mockiBookingRepository.Setup(x => x.GetBooking(It.IsAny<Guid>())).Returns(new BookingService.Booking
      {
        Hotel = new BookingService.Hotel
        {
          CountryCode = BookingService.Country.SE,
          Name = "Hotell connect"
        },
        Guests = new List<BookingService.Guest>{
          new BookingService.Guest()
          {
            Title = "VD",
            FirstName = "Anders",
            LastName = "Harge"
          },
          new BookingService.Guest()
          {
            Title = "herr",
            FirstName = "Per",
            LastName = "olofsson"
          }
        },
        RoomType = BookingService.RoomTyp.TRIPLE.ToString()
      });

      var addedGuest = new BookingService.Guest
      {
        FirstName = "Patrik",
        LastName = "Henriksson",
        Title = "SpringPojke"
      };

      _mockiBookingRepository.Setup(x => x.ReEnterBookingInformation(It.IsAny<string>())).Throws<Exception>();

      _sut.AddGuestToBooking(Guid.NewGuid(), addedGuest);

      _mockiBookingRepository.Verify(x => x.ReEnterBookingInformation(It.IsAny<string>()), Times.Never);
      _mockiBookingRepository.Verify(x => x.Save(It.IsAny<BookingService.Booking>()
        , It.IsAny<BookingService.Guest>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_To_Room_2_beeds_TWIN")]
    public void Test_of_AddGuestToBooking_hotell_DE_title_HerrTest_of_AddGuestToBooking_hotell_To_Room_2_beeds_TWIN()
    {
      _mockiBookingRepository.Setup(x => x.GetBooking(It.IsAny<Guid>())).Returns(new BookingService.Booking
      {
        Hotel = new BookingService.Hotel
        {
          CountryCode = BookingService.Country.SE,
          Name = "Hotell connect"
        },
        Guests = new List<BookingService.Guest>{
          new BookingService.Guest()
          {
            Title = "Kaffekokare",
            FirstName = "Anders",
            LastName = "Harge"
          }
        },
        RoomType = BookingService.RoomTyp.TWIN.ToString()
      });

      var addedGuest = new BookingService.Guest
      {
        FirstName = "Patrik",
        LastName = "Henriksson",
        Title = "SpringPojke"
      };

      _mockiBookingRepository.Setup(x => x.ReEnterBookingInformation(It.IsAny<string>())).Throws<Exception>();
      _sut.AddGuestToBooking(Guid.NewGuid(), addedGuest);

      _mockiBookingRepository.Verify(x => x.ReEnterBookingInformation(It.IsAny<string>()), Times.Never);
      _mockiBookingRepository.Verify(x => x.Save(It.IsAny<BookingService.Booking>()
        , It.IsAny<BookingService.Guest>()), Times.AtLeastOnce);
    }





  }


}
