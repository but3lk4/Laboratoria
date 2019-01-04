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
        public IQueryable<Booking> GetActiveBookings(int? bookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings = unitOfWork.Query<Booking>()
            .Where(
            b => b.Status != "Cancelled");

            if (bookingId.HasValue)
                bookings = bookings.Where(b => b.Id != bookingId.Value);

            return bookings;
        }
    }
}
