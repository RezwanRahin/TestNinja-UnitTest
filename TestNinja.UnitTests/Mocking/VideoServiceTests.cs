using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class VideoServiceTests
	{
		private VideoService _service;
		private Mock<IFileReader> _fileReader;
		private Mock<IVideoRepository> _repository;

		[SetUp]
		public void SetUp()
		{
			_fileReader = new Mock<IFileReader>();
			_repository = new Mock<IVideoRepository>();
			_service = new VideoService(_fileReader.Object, _repository.Object);
		}

		[Test]
		public void ReadVideoTitle_EmptyFile_ReturnError()
		{
			_fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

			var result = _service.ReadVideoTitle();

			Assert.That(result, Does.Contain("error").IgnoreCase);
		}

		[Test]
		public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnEmptyString()
		{
			_repository.Setup(r => r.GetUnprocessedVideosAsCsv()).Returns(new List<Video>());

			var result = _service.GetUnprocessedVideosAsCsv();

			Assert.That(result, Is.EqualTo(""));
		}

		[Test]
		public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnAStringWithIdOfIdOfUnprocessedVideo()
		{
			_repository.Setup(r => r.GetUnprocessedVideosAsCsv()).Returns(new List<Video>
			{
				new Video {Id = 1},
				new Video {Id = 2},
				new Video {Id = 3}
			});

			var result = _service.GetUnprocessedVideosAsCsv();

			Assert.That(result, Is.EqualTo("1,2,3"));
		}
	}
}
