﻿using System;
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
		private string _statementFileName;

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

			_statementFileName = "filename";
			_statementGenerator = new Mock<IStatementGenerator>();
			_statementGenerator.Setup(s => s.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate)).Returns(() => _statementFileName);

			_emailSender = new Mock<IEmailSender>();
			_messageBox = new Mock<IXtraMessageBox>();

			_service = new HouseKeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _messageBox.Object);
		}

		[Test]
		public void SendStatementEmails_WhenCalled_GenerateStatements()
		{
			_service.SendStatementEmails(_statementDate);

			_statementGenerator.Verify(s => s.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
		}

		[Test]
		public void SendStatementEmails_HouseKeepersEmailIsNull_ShouldNotGenerateStatements()
		{
			_houseKeeper.Email = null;

			_service.SendStatementEmails(_statementDate);

			_statementGenerator.Verify(s => s.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
		}

		[Test]
		public void SendStatementEmails_HouseKeepersEmailIsWhitespace_ShouldNotGenerateStatements()
		{
			_houseKeeper.Email = " ";

			_service.SendStatementEmails(_statementDate);

			_statementGenerator.Verify(s => s.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
		}

		[Test]
		public void SendStatementEmails_HouseKeepersEmailIsEmpty_ShouldNotGenerateStatements()
		{
			_houseKeeper.Email = "";

			_service.SendStatementEmails(_statementDate);

			_statementGenerator.Verify(s => s.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
		}

		[Test]
		[TestCase(null)]
		[TestCase("")]
		[TestCase(" ")]
		public void SendStatementEmails_HouseKeepersEmailIsNullOrWhiteSpaceOrEmpty_ShouldNotGenerateStatements(string email)
		{
			_houseKeeper.Email = email;

			_service.SendStatementEmails(_statementDate);

			_statementGenerator.Verify(s => s.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
		}

		[Test]
		public void SendStatementEmails_WhenCalled_EmailTheStatement()
		{
			_service.SendStatementEmails(_statementDate);

			VerifyEmailSent();
		}

		[Test]
		public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailTheStatement()
		{
			_statementFileName = null;

			_service.SendStatementEmails(_statementDate);

			VerifyEmailNotSent();
		}

		[Test]
		public void SendStatementEmails_StatementFileNameIsEmptyString_ShouldNotEmailTheStatement()
		{
			_statementFileName = "";

			_service.SendStatementEmails(_statementDate);

			VerifyEmailNotSent();
		}

		[Test]
		public void SendStatementEmails_StatementFileNameIsWhitespace_ShouldNotEmailTheStatement()
		{
			_statementFileName = " ";

			_service.SendStatementEmails(_statementDate);

			VerifyEmailNotSent();
		}

		[Test]
		public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
		{
			_emailSender.Setup(e => e.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();

			_service.SendStatementEmails(_statementDate);

			_messageBox.Verify(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
		}

		private void VerifyEmailSent()
		{
			_emailSender.Verify(e => e.EmailFile(_houseKeeper.Email, _houseKeeper.StatementEmailBody, _statementFileName, It.IsAny<string>()));
		}

		private void VerifyEmailNotSent()
		{
			_emailSender.Verify(e => e.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
		}
	}
}
