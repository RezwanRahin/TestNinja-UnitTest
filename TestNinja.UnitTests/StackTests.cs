using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public class StackTests
	{
		private Stack<string> _stack;

		[SetUp]
		public void SetUp()
		{
			_stack = new Stack<string>();
		}
	}
}
