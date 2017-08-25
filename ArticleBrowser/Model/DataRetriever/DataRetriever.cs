using System.Collections.Generic;
using System.Configuration;
using Dapper;
using MySql.Data.MySqlClient;

namespace WordAddIn1.Model.DataRetriever
{
	public class DataRetriever : BaseDataRetriever
	{
		/// <summary>
		/// Base constructor
		/// </summary>
		public DataRetriever()
		{
			var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
			base.Connection = new MySqlConnection(connectionString);
			// TODO: Test the DB structure, if it does not exist, create new
			var asd = RunQuery(GetTableNames);
		}

		private IEnumerable<dynamic> GetTableNames()
		{
			return Connection.Query<dynamic>("SHOW TABLES");
		}
	}
}