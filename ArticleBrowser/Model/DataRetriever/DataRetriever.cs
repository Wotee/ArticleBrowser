using System.Configuration;
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
		}
	}
}