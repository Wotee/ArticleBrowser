using System;
using System.IO;
using System.Linq;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using WordAddIn1.Model.DataRetriever;

namespace ArticleBrowserTest
{
	[TestFixture]
	public class DataRetrieverTests
	{
		private const string Host = "articlebrowserdb.csuwa1w47oj9.eu-central-1.rds.amazonaws.com";
		private const string Port = "3306";
		private const string Db = "articlebrowserdb";
		private const string Username = "ArticleBrowser";
		private const string Password = "password";

		[SetUp]
		public void Setup()
		{
			
		}

		[TearDown]
		public void TearDown()
		{
			try
			{
				// Delete the shit
			}
			catch (Exception)
			{
				Assume.That(false, "Teardown kuzi");
			}
		}

		[Test]
		public void TestDbConnection()
		{
			var connectionString = $"Server={Host};UID={Username};PWD={Password};DB={Db};Port={Port}";
			var connection = new MySqlConnection(connectionString);
		}
	}
}
