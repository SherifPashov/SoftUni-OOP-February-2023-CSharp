using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private IRepository<IHotel> hotels;
        
        public Controller()
        {
            hotels = new HotelRepository();
        }
        public string AddHotel(string hotelName, int category)
        {
            if (hotels.Select(hotelName)!=null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }
            Hotel hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);
            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
            
        }
        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
          
            if (hotels.Select(hotelName) == null) 
            { 
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            if (hotels.Select(hotelName).Rooms.GetType().Name==roomTypeName)
            {
                return OutputMessages.RoomTypeAlreadyCreated;
            }
            if (typeof(Apartment).Name!=roomTypeName &&
                typeof(DoubleBed).Name !=roomTypeName &&
                typeof(Studio).Name!=roomTypeName )
            
            {

                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect); 
            }
            
            if (typeof(Apartment).Name == roomTypeName)
            {
                IRoom room= new Apartment();
                hotels.Select(hotelName).Rooms.AddNew(room);
            }
            else if (typeof(DoubleBed).Name == roomTypeName)
            {
                IRoom room= new DoubleBed();
                hotels.Select(hotelName).Rooms.AddNew(room);
            }
            else if (typeof(Studio).Name == roomTypeName)
            {
                IRoom room= new Studio();
                hotels.Select(hotelName).Rooms.AddNew(room);
            }

            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            if (hotels.Select(hotelName) == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            if (typeof(Apartment).Name != roomTypeName &&
               typeof(DoubleBed).Name != roomTypeName &&
               typeof(Studio).Name != roomTypeName)

            {

                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            if (hotels.Select(hotelName).Rooms.All().FirstOrDefault(r =>r.GetType().Name == roomTypeName)==null) 
            {
                return OutputMessages.RoomTypeNotCreated;
            }
            if (hotels.Select(hotelName).Rooms.All().FirstOrDefault(r => r.GetType().Name == roomTypeName).PricePerNight!=0)
            {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);

            }
           
            

            hotels.Select(hotelName).Rooms.All().FirstOrDefault(r => r.GetType().Name == roomTypeName).SetPrice(price);
            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
            
           

        }
        public string BookAvailableRoom(int adultsCount, int childrenCount, int duration, int category)
        {
            if (this.hotels.All().FirstOrDefault(x => x.Category == category) == default)
            {
                return string.Format(OutputMessages.CategoryInvalid, category);
            }

            var orderedHotels =
                this.hotels.All().Where(x => x.Category == category).OrderBy(x => x.Turnover).ThenBy(x => x.FullName);


            foreach (var hotel in orderedHotels)
            {
                var selectedRoom = hotel.Rooms.All()
                    .Where(x => x.PricePerNight > 0)
                    .Where(y => y.BedCapacity >= adultsCount + childrenCount)
                    .OrderBy(z => z.BedCapacity).FirstOrDefault();

                if (selectedRoom != null)
                {
                    int bookingNumber = this.hotels.All().Sum(x => x.Bookings.All().Count) + 1;
                    IBooking booking = new Booking(selectedRoom, duration, adultsCount, childrenCount, bookingNumber);
                    hotel.Bookings.AddNew(booking);
                    return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName);
                }
            }

            return string.Format(OutputMessages.RoomNotAppropriate);
        }       

        public string HotelReport(string hotelName)
        {
            IHotel hotel = hotels.Select(hotelName);

            if (hotel == null)
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }


            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Hotel name: {hotel.FullName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine($"--Bookings:");
            sb.AppendLine();

            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
            }
            else
            {
                foreach (var booking in hotel.Bookings.All())
                {
                    sb.AppendLine($"{booking.BookingSummary()}");
                    sb.AppendLine();
                }
            }

            return sb.ToString().TrimEnd();
        }

       

       
    }
}
