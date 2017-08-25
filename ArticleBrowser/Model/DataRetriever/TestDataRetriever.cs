using System;
using System.Data.SQLite;
using System.IO;
using Dapper;

namespace WordAddIn1.Model.DataRetriever
{
	/// <summary>
	/// Helper to get data from SQLite database
	/// </summary>
	public class TestDataRetriever : BaseDataRetriever
	{
		// TODO: Check the comments, visibilities and regions
		#region Public Properties

		/// <summary>
		/// Filename to be used with db file and sql file
		/// </summary>
		private const string Filename = "Archiver";

		/// <summary>
		/// Path to DB File
		/// </summary>
		private static string DbFile => $"{AppDomain.CurrentDomain.BaseDirectory}{Filename}.db";

		/// <summary>
		/// Path to DB creation script
		/// </summary>
		private static string SqlFile => $"{AppDomain.CurrentDomain.BaseDirectory}{Filename}.sql";
		#endregion

		/// <summary>
		/// Default constructor, which creates the default .db file based on default .sql file
		/// </summary>
		public TestDataRetriever()
		{
			// TODO: Exception handling if files not found or something else bad happens?
			if (!File.Exists(DbFile))
				CreateDatabase();
			// TODO: Fix this later
			base.Connection = new SQLiteConnection("DataSource=" + DbFile + ";Version=3");
		}

		//~TestDataRetriever()
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

		#region Private Helper Methods

		/// <summary>
		/// Creates the database
		/// </summary>
		private void CreateDatabase()
		{
			try
			{
				var sql = File.ReadAllText(SqlFile);
				Connection.Execute(sql);
			}
			catch (Exception)
			{
				// TODO: Think about this
			}
		}
	}
	#endregion
}