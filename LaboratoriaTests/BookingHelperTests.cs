using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using Laboratoria;

namespace LaboratoriaTests
{
    // 1.nowa rezerwacja zaczyna i kończy się przed istniejącą; zwraca pusty string
    // 2.nowa rezerwacja zaczyna i kończy się w trakcie istniejącej; zwraca referencje
    // 3.nowa rezerwacja zaczyna i kończy się po istniejącej; zwraca referencje
    // 4.nowa rezerwacja zaczyna sie w trakcie istniejącej i kończy się po istniejącej; zwraca referencje
    // 5.nowa rezerwacja zaczyna sie w trakcie istniejącej i kończy się w trakcie istniejącej; zwraca referencje
    // 6.nowa rezerwacja zaczyna i kończy się po istniejącej; zwraca pusty string
    // 7.rezerwacja jest odwołana; zwraca pusty string
    
    [TestFixture]
    public class BookingHelperTests
    {
        private Booking _existingBooking;
        private Mock<IBookingRepository> _repository;

        [SetUp] //inicjalizuje obiekt booking 
        public void SetUp()
        {
            _existingBooking = new Booking { 
                Id = 2,
                ArrivalDate = ArriveOn(2019, 1, 9),
                DepartureDate = DepartOn(2019, 1, 14),
                Reference = "x"
            };

            _repository = new Mock<IBookingRepository>(); // dzięki Moq możemy zaimplementować interfejs IBookingRepository
            _repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
               _existingBooking

            }.AsQueryable()); // kiedy przywołujemy metodę GetActiveBookings powinno zwrócić listę bookingów
        }

        [Test]
        public void BookingStartsAndFinishesBefore_ReturnsEmptyString()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, 5),
                DepartureDate = Before(_existingBooking.ArrivalDate)

            }, _repository.Object);

            Assert.That(result, Is.Empty);

        }
        [Test]
        public void BookingStartsAndFinishesInTheMiddle_ReturnsExistingBookingRef()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),

            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));

        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfter_ReturnsExistingBookingRef()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),

            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));

        }
        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnBooking_ReturnsExistingBookingRef()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),

            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));

        }
        [Test]
        public void BookingStartsInTheMiddleOfAnBookingFinishesAfter_ReturnsExistingBookingRef()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),

            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));

        }

        [Test]
        public void BookingStartsAndFinishesAfterBooking_ReturnsEmptyString()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, 2),

            }, _repository.Object);

            Assert.That(result, Is.Empty);

        }

        [Test]
        public void BookingOverlapButNewBookingIsCancelled_ReturnsEmptyString()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Status = "Cancelled"
            }, _repository.Object);

            Assert.That(result, Is.Empty);

        }

    
        private DateTime ArriveOn(int year, int month, int day) // metoda pomocnicza, zwraca obiekt DateTime godzine przyjazdu
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
        private DateTime DepartOn(int year, int month, int day) // metoda pomocnicza, zwraca obiekt DateTime godzine odjazdu
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
        private DateTime Before(DateTime dateTime, int days = 1) // metoda pomocnicza, zwraca obiekt DateTime
        {
            return dateTime.AddDays(days);
        }
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

    }
}
