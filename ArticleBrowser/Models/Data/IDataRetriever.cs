using System.Collections.Generic;

namespace ArticleBrowserAddIn.Models.Data
{
	public interface IDataRetriever
	{
		IEnumerable<Item> GetItems();

		Item GetItem(string Title);

		void AddItem(Item item);
	}
}