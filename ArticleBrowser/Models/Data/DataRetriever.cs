using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Dapper;

namespace ArticleBrowserAddIn.Models.Data
{
	/// <summary>
	/// Helper to get data from SQLite database
	/// </summary>
	public class DataRetriever : IDataRetriever
	{
		// TODO: Check the comments, visibilities and regions
		#region Public Properties
		/// <summary>
		/// Filename to be used with db file and sql file
		/// </summary>
		private static string _filename = "Archiver";

		/// <summary>
		/// Extension for database
		/// </summary>
		private const string Db = ".db";

		/// <summary>
		/// Extension for database creation script
		/// </summary>
		private const string Sql = ".sql";

		/// <summary>
		/// Path to DB File
		/// </summary>
		private static string DbFile => AppDomain.CurrentDomain.BaseDirectory + _filename + Db;

		/// <summary>
		/// Path to DB creation script
		/// </summary>
		private static string SqlFile => AppDomain.CurrentDomain.BaseDirectory + _filename + Sql;
		// TODO: Change so both tests and prod run..
		/// <summary>
		/// Connection for the storageDB
		/// </summary>
		private static IDbConnection Connection => new SQLiteConnection("DataSource=" + DbFile + ";Version=3");
		private const string GetterSql = "SELECT * FROM Item I LEFT JOIN ItemCategory IC ON I.ID = IC.ItemID LEFT JOIN Category C ON IC.CategoryID = C.ID";

		#endregion

		#region Constructor
		/// <summary>
		/// Default constructor, which creates the default .db file based on default .sql file
		/// </summary>
		public DataRetriever()
		{
			// TODO: Exception handlings if files not found or something else bad happens?
			if (!File.Exists(DbFile))
				CreateDatabase();
		}

		/// <summary>
		/// Constructor, which creates the given .db file based on given .sql file
		/// </summary>
		/// <param name="filename">Filename to be used with both .db and .sql files</param>
		public DataRetriever(string filename)
		{
			// TODO: Exception handlings if files not found or something else bad happens?
			_filename = filename;
			if (!File.Exists(DbFile))
				CreateDatabase();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Get all of the items
		/// </summary>
		/// <returns>IEnumerable of items, or null if empty</returns>
		IList<Item> IDataRetriever.GetItems()
		{
			var lookup = new Dictionary<string, Item>();
			Connection.Query<Item, Category, Item>(GetterSql, (i, c) =>
			{
				Item item;
				if (!lookup.TryGetValue(i.Title, out item))
					lookup.Add(i.Title, item = i);
				item.Categories.Add(c); // TODO: This here was c
				return item;
			});
			return lookup.Values.ToList();
		}

		/// <summary>
		/// Adds single item to database
		/// </summary>
		/// <param name="item">Item to be added</param>
		void IDataRetriever.AddItem(Item item)
		{
			// TODO: Implement
			throw new NotImplementedException();
		}
		#endregion

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