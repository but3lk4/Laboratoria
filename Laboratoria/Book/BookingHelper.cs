using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoria
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository repository)
        {
            if (booking.Status == "Cancelled")  //sprawdzamy czy status Booking = cancelled, jeśli tak zwraca pusty string
                return string.Empty;

            var bookings = repository.GetActiveBookings(booking.Id); // pobiera aktywne rezerwacje
            var overlappingBooking = bookings.FirstOrDefault(
            b =>
            booking.ArrivalDate < b.DepartureDate
            && booking.ArrivalDate < b.DepartureDate);


            return overlappingBooking == null ? string.Empty
            : overlappingBooking.Reference; // zwraca pusty string jeśli nie nachodzą na siebie rezerwacje, w przeciwnym razie zwraca referencje
        }
    }

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }
    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}
