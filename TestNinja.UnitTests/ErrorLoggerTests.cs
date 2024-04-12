using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public class ErrorLoggerTests
	{
		private ErrorLogger _logger;

		[SetUp]
		public void SetUp()
		{
			_logger = new ErrorLogger();
		}

		[Test]
		public void Log_WhenCalled_SetTheLastErrorProperty()
		{
			var logger = new ErrorLogger();

			logger.Log("a");

			Assert.That(logger.LastError, Is.EqualTo("a"));
		}

		[Test]
		[TestCase(null)]
		[TestCase("")]
		[TestCase(" ")]
		public void Log_InvalidError_ThrowArgumentNullException(string error)
		{
			var logger = new ErrorLogger();

			Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
		}
	}
}
