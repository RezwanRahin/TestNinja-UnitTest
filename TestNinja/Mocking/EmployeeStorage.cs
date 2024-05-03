namespace TestNinja.Mocking
{
	public class EmployeeStorage
	{
		private EmployeeContext _db;

		public EmployeeStorage()
		{
			_db = new EmployeeContext();
		}
	}
}
