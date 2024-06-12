using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class HouseKeeperServiceTests
	{
		private HouseKeeperService _service;
		private Mock<IStatementGenerator> _statementGenerator;
		private Mock<IEmailSender> _emailSender;
		private Mock<IXtraMessageBox> _messageBox;
		private DateTime _statementDate = new DateTime(2017, 1, 1);
		private Housekeeper _houseKeeper;

		[SetUp]
		public void SetUp()
		{
			_houseKeeper = new Housekeeper
			{
				Email = "a",
				FullName = "b",
				Oid = 1,
				StatementEmailBody = "c"
			};

			var unitOfWork = new Mock<IUnitOfWork>();
			var housekeepers = new List<Housekeeper> { _houseKeeper };

			unitOfWork.Setup(u => u.Query<Housekeeper>()).Returns(housekeepers.AsQueryable);

			_statementGenerator = new Mock<IStatementGenerator>();
			_emailSender = new Mock<IEmailSender>();
			_messageBox = new Mock<IXtraMessageBox>();

			_service = new HouseKeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _messageBox.Object);
		}

		[Test]
		public void SendStatementEmails_WhenCalled_GenerateStatements()
		{
			var unitOfWork = new Mock<IUnitOfWork>();

			var housekeepers = new List<Housekeeper>
			{
				new Housekeeper
				{
					Email = "a",
					FullName = "b",
					Oid = 1,
					StatementEmailBody = "c"
				}
			};
			unitOfWork.Setup(u => u.Query<Housekeeper>()).Returns(housekeepers.AsQueryable);

			var statementGenerator = new Mock<IStatementGenerator>();
			var emailSender = new Mock<IEmailSender>();
			var messageBox = new Mock<IXtraMessageBox>();

			var service = new HouseKeeperService(unitOfWork.Object, statementGenerator.Object, emailSender.Object, messageBox.Object);


			service.SendStatementEmails(new DateTime(2017, 1, 1));

			statementGenerator.Verify(s => s.SaveStatement(1, "b", new DateTime(2017, 1, 1)));
		}
	}
}
