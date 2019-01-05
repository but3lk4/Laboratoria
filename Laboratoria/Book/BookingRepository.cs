using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoria
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? bookingId = null);
    }

    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? bookingId = null) /* nadajemy wartość null,
                                                                          by przy wywoływaniu metody nie trzeba było przesyłać int */
                                                                                
        {
            var unitOfWork = new UnitOfWork();
            var bookings = unitOfWork.Query<Booking>()
            .Where(
            b => b.Status != "Cancelled"); //sprawdzamy czy jest odwołana

            if (bookingId.HasValue)
                bookings = bookings.Where(b => b.Id != bookingId.Value); //

            return bookings;
        }
    }
}
