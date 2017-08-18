using System.Collections.Generic;

namespace ArticleBrowserAddIn.Models.Data
{
	public interface IDataRetriever
	{
		IList<Item> GetItems();

		void AddItem(Item item);
	}
}