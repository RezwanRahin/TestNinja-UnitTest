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
	}
}
