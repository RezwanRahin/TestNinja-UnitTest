using System.Net;

namespace TestNinja.Mocking
{
	public class FileDownloader
	{
		public void DownloadFile(string url, string path)
		{
			var client = new WebClient();

			client.DownloadFile(url, path);
		}
	}
}
