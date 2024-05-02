using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class InstallerHelperTests
	{
		private Mock<IFileDownloader> _fileDownloader;
		private InstallerHelper _installerHelper;

		[SetUp]
		public void SetUp()
		{
			_fileDownloader = new Mock<IFileDownloader>();
			_installerHelper = new InstallerHelper(_fileDownloader.Object);
		}
	}
}
