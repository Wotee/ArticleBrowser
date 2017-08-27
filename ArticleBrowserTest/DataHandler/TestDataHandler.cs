using System;
using System.Data.SQLite;
using System.IO;
using WordAddIn1.Model.DataHandler;

namespace ArticleBrowserTest.DataHandler
{
	/// <summary>
	/// Helper to get data from SQLite database
	/// </summary>
	public class TestDataHandler : BaseDataHandler
	{
		#region Public Properties
		/// <summary>
		/// Filename to be used with db file and sql file
		/// </summary>
		private const string Filename = "Archiver";

		/// <summary>
		/// Path to DB File
		/// </summary>
		private static string DbFile => $"{AppDomain.CurrentDomain.BaseDirectory}{Filename}.db";
		#endregion

		/// <summary>
		/// Default constructor, which creates the default .db file based on default .sql file
		/// </summary>
		public TestDataHandler()
		{
			if (!File.Exists(DbFile))
				CreateDataBase();
			Connection = new SQLiteConnection("DataSource=" + DbFile + ";Version=3");
		}

		// TODO: Add this later, when creation script works again for the test db
		//~TestDataHandler()
		//{
		//	try
		//	{
		//		File.Delete(DbFile);
		//	}
		//	catch (Exception)
		//	{
		//		// This is very unfortunate...
		//	}
		//}
	}
}