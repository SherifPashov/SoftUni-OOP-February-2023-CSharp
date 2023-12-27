using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void RoomConstructor()
        {
            Room room = new Room(3, 50);
            Assert.AreEqual(3, room.BedCapacity);
            Assert.AreEqual(50, room.PricePerNight);
        }

        [TestCase(0)]
        [TestCase(-10)]
        [TestCase(-5)]
        public void RoomBedCapacityuncarectly(int capacity)
        {
            Assert.Throws<ArgumentException>(() => new Room(capacity, 50));

        }

        [TestCase(0)]
        [TestCase(-10)]
        [TestCase(-5)]
        public void RoomPricePerNightuncarectly(int pricePerNight)
        {
            Assert.Throws<ArgumentException>(() => new Room(3, pricePerNight));

        }



        [Test]
        public void BookingConstructor()
        {
            Room room = new Room(3, 50);
            Booking booking = new Booking(5, room, 4);

            Assert.AreEqual(5, booking.BookingNumber);
            Assert.AreEqual(4, booking.ResidenceDuration);
            Assert.AreEqual(3, booking.Room.BedCapacity);
            Assert.AreEqual(50, booking.Room.PricePerNight);




        }

        [Test]
        public void HotelConstructor()
        {
            Hotel hotel = new Hotel("Kachemak", 4);
            Assert.AreEqual("Kachemak", hotel.FullName);
            Assert.AreEqual(4, hotel.Category);
            Assert.AreEqual(0, hotel.Rooms.Count);
            Assert.AreEqual(0, hotel.Rooms.Count);

        }

        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]

        public void HotelFullNameThrowWhenNullOrWiteSpase(string fullName)
        {
            Assert.Throws<ArgumentNullException>(() => new Hotel(fullName, 4));

        }

        [TestCase(0)]
        [TestCase(-3)]
        [TestCase(6)]
        [TestCase(10)]

        public void HotelCategoryThrowExseptionWheLes0AndMore5Stars(int category)
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Kachemak", category));

        }
        [Test]
        public void HotelAddRoomCorectly()
        {
            Hotel hotel = new Hotel("Kachemak", 4);
            Room room = new Room(3, 50);
            hotel.AddRoom(room);
            Assert.AreEqual(1, hotel.Rooms.Count);
            hotel.AddRoom(room);
            Assert.AreEqual(2, hotel.Rooms.Count);
        }

        [TestCase(0)]
        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(-3)]
        public void HotelBookRoomAdultsLesOneThrewExseption(int adults)
        {
            Hotel hotel = new Hotel("Kachemak", 4);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(adults, 3, 3, 30));
        }

        
        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(-3)]
        public void HotelBookRoomchildrenLesZeroThrewExseption(int childeren)
        {
            Hotel hotel = new Hotel("Kachemak", 4);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(3, childeren, 3, 30));
        }

        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(-3)]
        public void HotelBookRoomResidenceDurationLesZeroThrewExseption(int residenceDuration)
        {
            Hotel hotel = new Hotel("Kachemak", 4);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(3, 3, residenceDuration, 30));
        }

        [TestCase]
        public void HotelBookRoomCorrectBudjet()
        {
            Hotel hotels = new Hotel("Kachemak", 4);
            Room room = new Room(3, 50);
            hotels.AddRoom(room);
            hotels.BookRoom(1, 2 ,3, 300);
            
            Assert.AreEqual(1,hotels.Bookings.Count);
            Assert.AreEqual(150,hotels.Turnover);




            
        }

    }
}