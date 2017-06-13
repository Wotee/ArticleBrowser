using System;
using System.IO;
using System.Linq;
using ArticleBrowserAddIn.Models.Data;
using NUnit.Framework;

namespace ArticleBrowserTest
{
	[TestFixture]
	public class DataRetrieverTests
	{
		private static readonly string Filename = @"\Tests";
		private static readonly string DB = ".db";

		private IDataRetriever _retriever;

		[OneTimeSetUp]
		public void Setup()
		{
			_retriever = new DataRetriever(Filename);
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			File.Delete(Filename + DB);
		}

		[Test]
		public void GetSingleItemSingleCategoryTest()
		{
			var item = _retriever.GetItem("Test Title 1");
			Assert.AreEqual(item.Categories?.Count, 2);
		}

		[Test]
		public void GetSingleItemMultipleCategoriesTest()
		{
			var item = _retriever.GetItem("Test Title 2");
			Assert.AreEqual(item.Categories?.Count, 1);
		}

		[Test]
		public void GetItems()
		{
			var items = _retriever.GetItems();
			Assert.AreEqual(2, items.Count());
			foreach (var item in items)
			{
				Assert.GreaterOrEqual(item.Categories?.Count, 1);
			}
		}
	}
}
