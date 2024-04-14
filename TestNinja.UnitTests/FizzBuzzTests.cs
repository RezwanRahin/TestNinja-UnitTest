using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public class FizzBuzzTests
	{
		[Test]
		public void GetOutput_InputIsDivisibleBy3And5_ReturnFizzBuzz()
		{
			var result = FizzBuzz.GetOutput(15);

			Assert.That(result, Is.EqualTo("FizzBuzz"));
		}
	}
}
