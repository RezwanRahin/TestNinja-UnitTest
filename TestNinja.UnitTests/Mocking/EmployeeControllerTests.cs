using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class EmployeeControllerTests
	{
		[Test]
		public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb()
		{
			var storage = new Mock<IEmployeeStorage>();
			var controller = new EmployeeController(storage.Object);

			controller.DeleteEmployee(1);

			storage.Verify(s => s.DeleteEmployee(1));
		}

		[Test]
		public void DeleteEmployee_WhenCalled_ReturnRedirectResultObject()
		{
			var storage = new Mock<IEmployeeStorage>();
			var controller = new EmployeeController(storage.Object);

			var result = controller.DeleteEmployee(1);

			Assert.That(result, Is.EqualTo(new RedirectResult()));
		}
	}
}
