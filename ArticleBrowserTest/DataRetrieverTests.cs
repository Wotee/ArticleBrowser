using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace ArticleBrowserTest
{
	[TestFixture]
	public class DataRetrieverTests
	{


		[TestCaseSource(typeof(Data), nameof(Data.TestData))]
		public void TestDbConnection(string connectionString)
		{
			Assume.That(!string.IsNullOrEmpty(connectionString), "Connection string was invalid");
			var connection = new MySqlConnection(connectionString);
			var result = connection.Query("SELECT * FROM user");
			Assert.That(result, Is.Not.Empty);
		}
	}

	public static class Data
	{
		public static IEnumerable<TestCaseData> TestData
		{
			get
			{
				yield return new TestCaseData("Server=articlebrowserdb.csuwa1w47oj9.eu-central-1.rds.amazonaws.com;UID=ArticleBrowser;PWD=password;Database=mysql;Port=3306").SetName("Valid Connection String");
				yield return new TestCaseData("").SetName("Invalid connection string");
			}
		}
	}
}
