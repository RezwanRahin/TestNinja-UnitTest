using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class BookingHelperTests_OverlappingBookingsExistTests
	{
		[Test]
		public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
		{
			var repository = new Mock<IBookingRepository>();
			var bookings = new List<Booking> {
				new Booking
				{
					Id = 2,
					ArrivalDate = new DateTime(2017, 1, 15, 14, 0, 0),
					DepartureDate = new DateTime(2017, 1, 20, 10, 0, 0),
					Reference = "a"
				}
			};
			repository.Setup(m => m.GetActiveBookings(1)).Returns(bookings.AsQueryable);


			var booking = new Booking
			{
				Id = 1,
				ArrivalDate = new DateTime(2017, 1, 10, 14, 0, 0),
				DepartureDate = new DateTime(2017, 1, 14, 10, 0, 0),
			};
			var result = BookingHelper.OverlappingBookingsExist(booking, repository.Object);

			Assert.That(result, Is.Empty);
		}

		private DateTime ArriveOn(int year, int month, int day)
		{
			return new DateTime(year, month, day, 14, 0, 0);
		}

		private DateTime DepartOn(int year, int month, int day)
		{
			return new DateTime(year, month, day, 10, 0, 0);
		}
	}
}
