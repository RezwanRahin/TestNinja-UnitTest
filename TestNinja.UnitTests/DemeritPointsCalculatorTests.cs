using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public class DemeritPointsCalculatorTests
	{
		private DemeritPointsCalculator _calculator;

		[SetUp]
		public void SetUp()
		{
			_calculator = new DemeritPointsCalculator();
		}

		[Test]
		[TestCase(-1)]
		[TestCase(301)]
		public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowArgumentOutOfRangeOfRangeArgument(int speed)
		{
			Assert.That(() => _calculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
		}
	}
}
