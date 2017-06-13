using System;
using System.Collections.Generic;
using ArticleBrowserAddIn.Models.Data;

namespace ArticleBrowserAddIn.Models.Mocks
{
	// TODO: Check if this mock version is really needed
	public class MockDataRetriever : IDataRetriever
	{
		public Item GetItem(string Title)
		{
			throw new NotImplementedException();
		}

		public void AddItem(Item item)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Item> GetItems()
		{
			return new List<Item> { new Item() };
		}
	}
}