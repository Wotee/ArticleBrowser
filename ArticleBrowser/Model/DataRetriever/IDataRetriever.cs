using System.Collections.Generic;

namespace WordAddIn1.Model.DataRetriever
{
	public interface IDataRetriever
	{
		IList<Item> GetItems();

		void AddItem(Item item);
	}
}