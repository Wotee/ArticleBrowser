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
		private static string Filename = "Archiver";
		/// <summary>
		/// Extension for database
		/// </summary>
		private static readonly string DB = ".db";
		/// <summary>
		/// Extension for database creation script
		/// </summary>
		private static readonly string SQL = ".sql";
		/// <summary>
		/// Path to DB File
		/// </summary>
		private static string DbFile => AppDomain.CurrentDomain.BaseDirectory + Filename + DB;

		/// <summary>
		/// Path to DB creation script
		/// </summary>
		private static string SqlFile => AppDomain.CurrentDomain.BaseDirectory + Filename + SQL;
		// TODO: Change so both tests and prod run..
		/// <summary>
		/// Connection for the storageDB
		/// </summary>
		private static IDbConnection Connection => new SQLiteConnection("DataSource=" + DbFile + ";Version=3");

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
			Filename = filename;
			if (!File.Exists(DbFile))
				CreateDatabase();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Gets all of the items
		/// </summary>
		/// <returns>IEnumerable of items, or null if empty</returns>
		IEnumerable<Item> IDataRetriever.GetItems()
		{
			string sql =
				"SELECT * " +
				"FROM Item I " +
				"INNER JOIN ItemCategory IC " +
				"ON I.ID = IC.ItemID " +
				"INNER JOIN Category C " +
				"ON IC.CategoryID = C.ID";
			var lookup = new Dictionary<string, Item>();
			Connection.Query<Item, Category, Item>(sql, (i, c) =>
				{
					Item item;
					if (!lookup.TryGetValue(i.Title, out item))
						lookup.Add(i.Title, item = i);
					if (item.Categories == null)
						item.Categories = new List<Category>();
					item.Categories.Add(c);
					return item;
				});
			return lookup.Values;
		}

		/// <summary>
		/// Gets single item based on title
		/// </summary>
		/// <param name="Title">Title to be searched</param>
		/// <returns></returns>
		Item IDataRetriever.GetItem(string title)
		{
			string sql =
				"SELECT * " +
				"FROM Item I " +
				"INNER JOIN ItemCategory IC " +
				"ON I.ID = IC.ItemID " +
				"INNER JOIN Category C " +
				"ON IC.CategoryID = C.ID " +
				"WHERE I.Title = @title";
			var lookup = new Dictionary<string, Item>();

			Connection.Query<Item, Category, Item>(sql, (i, c) =>
				{
					Item item;
					if (!lookup.TryGetValue(i.Title, out item))
						lookup.Add(i.Title, item = i);
					if (item.Categories == null)
						item.Categories = new List<Category>();
					item.Categories.Add(c);
					return item;
				},
				new { Title = title });
			return lookup.Values.FirstOrDefault();
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