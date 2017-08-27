using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace WordAddIn1.Model.DataHandler
{
	public class DataHandler : BaseDataHandler
	{
		/// <summary>
		/// Base constructor
		/// </summary>
		public DataHandler()
		{
			var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
			base.Connection = new MySqlConnection(connectionString);
			if (!CheckDBValidity(GetTableNames()))
			{
				CreateDataBase();
			}
		}

		/// <summary>
		/// Get table names in database
		/// </summary>
		/// <returns><see cref="IEnumerable{T}"/> of table names</returns>
		private IEnumerable<string> GetTableNames()
		{
			return Connection.Query<string>("SHOW TABLES");
		}

		/// <summary>
		/// Checks that database structure is valid
		/// </summary>
		/// <param name="dbStructure"><see cref="IEnumerable{T}"/> of table names</param>
		/// <returns>True if database is valid</returns>
		private bool CheckDBValidity(IEnumerable<string> dbStructure)
		{
			var tempTables = dbStructure.ToList();

			if (!tempTables.Contains("Item")) return false;
			if (!tempTables.Contains("Category")) return false;
			if (!tempTables.Contains("ItemCategory")) return false;
			return true;
		}
	}
}